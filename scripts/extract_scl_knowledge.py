"""
Extract SCL help instructions from PDF into chunked markdown files.

Usage:
    python extract_scl_knowledge.py

Reads: SCL_help_instructions_s7_1200_1500.pdf (from project root)
Writes: tia-agent/docs/knowledge-base/<category>/<instruction>.md
"""

import os
import re
import pdfplumber
from pathlib import Path

PROJECT_ROOT = Path(__file__).resolve().parent.parent.parent
PDF_PATH = PROJECT_ROOT / "SCL_help_instructions_s7_1200_1500.pdf"
OUTPUT_DIR = PROJECT_ROOT / "tia-agent" / "docs" / "knowledge-base"


def slugify(text: str) -> str:
    """Convert text to a filesystem-safe slug."""
    text = text.lower()
    text = re.sub(r"\(s7-[^)]*\)", "", text)  # Remove (S7-...) suffixes
    text = text.strip().rstrip(":")
    text = re.sub(r"[^a-z0-9]+", "-", text)
    return text.strip("-")


def extract_pages(pdf) -> list[dict]:
    """Extract text from all pages."""
    pages = []
    for i, page in enumerate(pdf.pages):
        text = page.extract_text() or ""
        pages.append({"page_num": i + 1, "text": text})
    return pages


def identify_sections(pages: list[dict]) -> list[dict]:
    """
    Identify section boundaries. Each page's first line is checked
    for the instruction header pattern.
    """
    sections = []

    for p in pages:
        text = p["text"].strip()
        if not text:
            continue

        lines = text.split("\n")
        first_line = lines[0].strip()

        # Category pages contain "This section contains information"
        is_category = "This section contains information" in text

        # Detect header: "Name: Description (S7-...)" or "Name (S7-...)"
        has_platform_tag = bool(re.search(r"\(S7-\d+", first_line))
        # Also match pages without platform tag but that start a new topic
        # (continuation pages repeat the same header)

        if is_category:
            sections.append({
                "type": "category",
                "title": first_line,
                "name": re.sub(r"\s*\(S7-[^)]*\)", "", first_line).strip(),
                "start_page": p["page_num"],
                "text": text,
            })
        elif has_platform_tag:
            # Clean name: remove platform tag and trailing description after colon
            raw_name = re.sub(r"\s*\(S7-[^)]*\)", "", first_line).strip()
            sections.append({
                "type": "instruction",
                "title": first_line,
                "name": raw_name,
                "start_page": p["page_num"],
                "text": text,
            })
        else:
            # Continuation page — append to previous section
            if sections:
                sections[-1]["text"] += "\n\n" + text

    return sections


def group_by_category(sections: list[dict]) -> dict[str, list[dict]]:
    """Group instruction sections under their parent category."""
    grouped = {}
    current_category = "General"

    for s in sections:
        if s["type"] == "category":
            current_category = s["name"]
            if current_category not in grouped:
                grouped[current_category] = []
        elif s["type"] == "instruction":
            if current_category not in grouped:
                grouped[current_category] = []
            # Deduplicate: merge if same name already exists in this category
            existing = next(
                (x for x in grouped[current_category] if x["name"] == s["name"]),
                None,
            )
            if existing:
                existing["text"] += "\n\n" + s["text"]
            else:
                grouped[current_category].append(s)

    return grouped


def write_markdown(category: str, instruction: dict, output_dir: Path):
    """Write a single instruction as a markdown file."""
    cat_slug = slugify(category)
    cat_dir = output_dir / cat_slug
    cat_dir.mkdir(parents=True, exist_ok=True)

    inst_slug = slugify(instruction["name"])
    if not inst_slug:
        inst_slug = f"page-{instruction['start_page']}"

    filepath = cat_dir / f"{inst_slug}.md"

    # If file already exists (slug collision), append page number
    if filepath.exists():
        filepath = cat_dir / f"{inst_slug}-p{instruction['start_page']}.md"

    # Clean up the text: remove page number footers like "- 42 -"
    text = re.sub(r"\n- \d+ -\s*$", "", instruction["text"], flags=re.MULTILINE)

    content = f"# {instruction['name']}\n\n"
    content += f"**Category:** {category}  \n"
    content += f"**Source:** SCL_help_instructions_s7_1200_1500.pdf, page {instruction['start_page']}  \n\n"
    content += "---\n\n"
    content += text

    filepath.write_text(content, encoding="utf-8")
    return filepath


def write_index(grouped: dict, output_dir: Path):
    """Write an index file listing all categories and instructions."""
    lines = ["# SCL Knowledge Base\n"]
    lines.append("Auto-generated from SCL_help_instructions_s7_1200_1500.pdf\n\n")

    total = 0
    for category, instructions in grouped.items():
        cat_slug = slugify(category)
        lines.append(f"## {category}\n")
        for inst in instructions:
            inst_slug = slugify(inst["name"])
            if not inst_slug:
                inst_slug = f"page-{inst['start_page']}"
            lines.append(f"- [{inst['name']}]({cat_slug}/{inst_slug}.md)")
            total += 1
        lines.append("")

    lines.insert(2, f"**{total} instructions** across **{len(grouped)} categories**\n")

    index_path = output_dir / "INDEX.md"
    index_path.write_text("\n".join(lines), encoding="utf-8")
    return index_path


def main():
    print(f"Reading PDF: {PDF_PATH}")
    pdf = pdfplumber.open(str(PDF_PATH))

    print(f"Extracting {len(pdf.pages)} pages...")
    pages = extract_pages(pdf)

    print("Identifying sections...")
    sections = identify_sections(pages)
    categories = [s for s in sections if s["type"] == "category"]
    instructions = [s for s in sections if s["type"] == "instruction"]
    print(f"  Found {len(categories)} categories, {len(instructions)} raw instruction sections")

    print("Grouping and deduplicating...")
    grouped = group_by_category(sections)
    total = sum(len(v) for v in grouped.values())
    print(f"  {total} unique instructions across {len(grouped)} categories")

    # Clean output dir
    if OUTPUT_DIR.exists():
        import shutil
        shutil.rmtree(OUTPUT_DIR)
    OUTPUT_DIR.mkdir(parents=True)

    print(f"Writing markdown files to {OUTPUT_DIR}...")
    file_count = 0
    for category, instructions in grouped.items():
        for inst in instructions:
            write_markdown(category, inst, OUTPUT_DIR)
            file_count += 1

    index_path = write_index(grouped, OUTPUT_DIR)
    print(f"  Wrote {file_count} instruction files + index")
    print(f"  Index: {index_path}")
    print("Done!")


if __name__ == "__main__":
    main()
