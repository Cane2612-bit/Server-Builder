using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class RoundedPictureBox : PictureBox
{
    // Fields
    private int bordersize = 0;
    private int borderradius = 40;
    private Color bordercolor = Color.PaleVioletRed;

    [Category("RoundedPictureBox")]
    public int Bordersize
    {
        get { return bordersize; }
        set
        {
            bordersize = value;
            this.Invalidate();
        }
    }

    [Category("RoundedPictureBox")]
    public int Borderradius
    {
        get { return borderradius; }
        set
        {
            borderradius = value;
            this.Invalidate();
        }
    }

    [Category("RoundedPictureBox")]
    public Color Bordercolor
    {
        get { return bordercolor; }
        set
        {
            bordercolor = value;
            this.Invalidate();
        }
    }

    // Constructor
    public RoundedPictureBox()
    {
        this.Size = new Size(150, 150);
        this.BackColor = Color.MediumSlateBlue;
        this.SizeMode = PictureBoxSizeMode.StretchImage;
    }

    // Methods
    private GraphicsPath GetFigurePath(RectangleF rect, float radius)
    {
        GraphicsPath path = new GraphicsPath();
        path.StartFigure();
        path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
        path.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90);
        path.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90);
        path.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90);
        path.CloseFigure();
        return path;
    }

    protected override void OnPaint(PaintEventArgs pevent)
    {
        base.OnPaint(pevent);
        pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        RectangleF rectSurface = new RectangleF(0, 0, this.Width, this.Height);
        RectangleF rectBorder = new RectangleF(1, 1, this.Width - 0.8F, this.Height - 1);

        if (borderradius > 2) // Rounded picture box
        {
            using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderradius))
            using (GraphicsPath pathBorder = GetFigurePath(rectBorder, borderradius - 1F))
            using (Pen penSurface = new Pen(this.Parent.BackColor, 2))
            using (Pen penBorder = new Pen(bordercolor, bordersize))
            {
                penBorder.Alignment = PenAlignment.Inset;
                // Picture box surface
                this.Region = new Region(pathSurface);
                // Draw surface border
                pevent.Graphics.DrawPath(penSurface, pathSurface);
                // Draw control border
                // Picture box border
                if (bordersize >= 1)
                    pevent.Graphics.DrawPath(penBorder, pathBorder);
            }
        }
        else // Normal picture box
        {
            this.Region = new Region(rectSurface);
            if (bordersize >= 1)
            {
                using (Pen penBorder = new Pen(bordercolor, bordersize))
                    pevent.Graphics.DrawRectangle(penBorder, 0, 0, this.Width - 1, this.Height - 1);
            }
        }
    }
}