using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;


//Free Resource Online
public class PdfExporter
{
    private DataGridView _dgv;
    private string _title;
    private int _currentRow = 0;

    public PdfExporter(DataGridView dgv, string title)
    {
        _dgv = dgv;
        _title = title;
    }

    public void Print()
    {
        PrintDocument pd = new PrintDocument();
        pd.PrintPage += Pd_PrintPage;

        PrintPreviewDialog preview = new PrintPreviewDialog
        {
            Document = pd,
            Width = 1000,
            Height = 700
        };

        preview.ShowDialog();
    }

    private void Pd_PrintPage(object sender, PrintPageEventArgs e)
    {
        float y = 50;
        float leftMargin = e.MarginBounds.Left;
        float pageWidth = e.MarginBounds.Width;
        int colCount = _dgv.Columns.Count;

        Font titleFont = new Font("Arial", 14, FontStyle.Bold);
        Font headerFont = new Font("Arial", 9, FontStyle.Bold);
        Font contentFont = new Font("Arial", 8);

        // Title (centered)
        RectangleF titleRect = new RectangleF(leftMargin, y, pageWidth, 30);
        StringFormat titleFormat = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };
        e.Graphics.DrawString(_title, titleFont, Brushes.Black, titleRect, titleFormat);
        y += 40;

        // Calculate column width dynamically
        float colWidth = pageWidth / colCount;

        // Draw headers (left-justified with ellipsis)
        for (int i = 0; i < colCount; i++)
        {
            string headerText = _dgv.Columns[i].HeaderText;
            RectangleF rect = new RectangleF(leftMargin + i * colWidth, y, colWidth, 20);

            StringFormat format = new StringFormat
            {
                Alignment = StringAlignment.Near,  // Left-justify
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisCharacter
            };

            e.Graphics.DrawString(headerText, headerFont, Brushes.Black, rect, format);
            e.Graphics.DrawRectangle(Pens.Black, leftMargin + i * colWidth, y, colWidth, 20);
        }
        y += 20;

        // Draw rows
        while (_currentRow < _dgv.Rows.Count)
        {
            DataGridViewRow row = _dgv.Rows[_currentRow];
            if (row.IsNewRow)
            {
                _currentRow++;
                continue;
            }

            float rowHeight = 20;

            for (int i = 0; i < colCount; i++)
            {
                string text = row.Cells[i].Value?.ToString() ?? "";
                RectangleF rect = new RectangleF(leftMargin + i * colWidth, y, colWidth, rowHeight);

                StringFormat format = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center,
                    Trimming = StringTrimming.EllipsisCharacter
                };

                e.Graphics.DrawString(text, contentFont, Brushes.Black, rect, format);
                e.Graphics.DrawRectangle(Pens.Black, leftMargin + i * colWidth, y, colWidth, rowHeight);
            }

            y += rowHeight;

            // Page break
            if (y + 40 > e.MarginBounds.Bottom)
            {
                e.HasMorePages = true;
                _currentRow++;
                return;
            }

            _currentRow++;
        }

        // Footer
        y = e.MarginBounds.Bottom - 30;
        e.Graphics.DrawString("Generated on: " + DateTime.Now, contentFont, Brushes.Black, leftMargin, y);

        e.HasMorePages = false;
        _currentRow = 0; // Reset for next print
    }
}
