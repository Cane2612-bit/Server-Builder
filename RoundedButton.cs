﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

[ToolboxItem(true)]
public class RoundedButton : Button
{
    //fields
    private int bordersize = 0;
    private int borderradius = 40;
    private Color bordercolor = Color.PaleVioletRed;

    [Category("RoundedButton")]
    public int Bordersize
    {
        get
        {
            return bordersize;
        }
        set
        {
            bordersize = value;
            this.Invalidate();
        }
    }
    [Category("RoundedButton")]
    public int Borderradius
    {
        get
        {
            return borderradius;
        }
        set
        {
            borderradius = value;
            this.Invalidate();
        }
    }
    [Category("RoundedButton")]
    public Color Bordercolor
    {
        get
        {
            return bordercolor;
        }
        set
        {
            bordercolor = value;
            this.Invalidate();
        }
    }


    [Category("RoundedButton")]
    public Color BackgroundColor
    {
        get { return this.BackColor; }
        set { this.BackColor = value; }
    }

    [Category("RoundedButton")]
    public Color TextColor
    {
        get { return this.ForeColor; }
        set { this.ForeColor = value; }
    }


    //constructor
    public RoundedButton()
    {
        this.FlatStyle = FlatStyle.Flat;
        this.FlatAppearance.BorderSize = 0;
        this.Size = new Size(150, 40);
        this.BackColor = Color.MediumSlateBlue;
        this.ForeColor = Color.White;
    }

    // methods
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
        RectangleF RectBorder = new RectangleF(1, 1, this.Width - 0.8F, this.Height - 1);

        if (borderradius > 2)// rounded button
        {
            using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderradius))
            using (GraphicsPath pathborder = GetFigurePath(RectBorder, borderradius - 1F))
            using (Pen pensurface = new Pen(this.Parent.BackColor, 2))
            using (Pen penBorder = new Pen(bordercolor, bordersize))
            {
                penBorder.Alignment = PenAlignment.Inset;
                // button surface
                this.Region = new Region(pathSurface);
                // Draw surface border
                pevent.Graphics.DrawPath(pensurface, pathSurface);
                // Draw control border
                // button border
                if (bordersize >= 1)
                    pevent.Graphics.DrawPath(penBorder, pathborder);
            }
        }
        else // normal button
        {
            this.Region = new Region(rectSurface);
            if (bordersize >= 1)
            {
                using (Pen penBorder = new Pen(bordercolor, bordersize))
                    pevent.Graphics.DrawRectangle(penBorder, 0, 0, this.Width - 1, this.Height - 1);
            }
        }
    }
    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        this.Parent.BackColorChanged += new EventHandler(Container_BackColorChanged);
    }

    private void Container_BackColorChanged(object sender, EventArgs e)
    {
        if (this.DesignMode)
            this.Invalidate();
    }
}