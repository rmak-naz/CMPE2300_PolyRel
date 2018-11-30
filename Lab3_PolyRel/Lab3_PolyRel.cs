﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GDIDrawer;

namespace Lab3_PolyRel
{
    public partial class Lab3_PolyRel : Form
    {
        CDrawer canvas = new CDrawer(1000, 1000);
        Fungus GreenFungus, RedFungus, BlueFungus;
        List<Shape> _shapes = new List<Shape>();

        private void ShapeTick_Tick(object sender, EventArgs e)
        {

        }

        public Lab3_PolyRel()
        {
            InitializeComponent();
        }

        private void Lab3_PolyRel_Load(object sender, EventArgs e)
        {
            GreenFungus = new Fungus(new Point(0, 0), canvas, Fungus.FungusColor.Green);
            RedFungus = new Fungus(new Point(0, 0), canvas, Fungus.FungusColor.Red);
            BlueFungus = new Fungus(new Point(0, 0), canvas, Fungus.FungusColor.Blue);

            //center solid anchors
            _shapes.Add(new FixedSquare(new Point(450, 500), Color.Red, null));
            _shapes.Add(new FixedSquare(new Point(550, 500), Color.Red, _shapes[0]));
            // ccw orbit chain
            {
                List<Shape> local = new List<Shape>();
                local.Add(new OrbitBall(Color.Yellow, 50, _shapes[0], -0.1, Math.PI));
                local.Add(new OrbitBall(Color.Pink, 50, local[0], -0.15, Math.PI));
                local.Add(new OrbitBall(Color.Blue, 50, local[1], -0.2, Math.PI));
                local.Add(new OrbitBall(Color.Green, 50, local[2], -0.25, Math.PI));
                _shapes.AddRange(local);
            }
            // cw orbit chain
            {
                List<Shape> local = new List<Shape>();
                local.Add(new OrbitBall(Color.Yellow, 50, _shapes[1], 0.05));
                local.Add(new OrbitBall(Color.Pink, 50, local[0], 0.075));
                local.Add(new OrbitBall(Color.Blue, 50, local[1], 0.1));
                local.Add(new OrbitBall(Color.Green, 50, local[2], 0.125));
                _shapes.AddRange(local);
            }
            // fixed/double h/v wobble + orbit chain
            {
                List<Shape> local = new List<Shape>();
                local.Add(new FixedSquare(new PointF(200, 500), Color.Cyan, null));
                local.Add(new VWobbleBall(Color.Red, 100, local[0], 0.1));
                local.Add(new HWobbleBall(Color.Red, 100, local[1], 0.15));
                local.Add(new OrbitBall(Color.LightBlue, 25, local[2], 0.2));
                _shapes.AddRange(local);
            }
            // show the top row of solid blocks with incremental offset animated vwballs
            {
                List<Shape> localA = new List<Shape>();
                List<Shape> localB = new List<Shape>();
                for (int i = 50; i < 1000; i += 50)
                    localA.Add(new FixedSquare(new PointF(i, 100), Color.Cyan, null));
                _shapes.AddRange(localA);
                double so = 0;
                foreach (Shape s in localA)
                    localB.Add(new VWobbleBall(Color.Purple, 50, s, 0.1, so += 0.7));
                _shapes.AddRange(localB);
            }

            // show 3-tier cloud of quad balls orbiting the same block
            {
                List<Shape> local = new List<Shape>();
                local.Add(new FixedSquare(new PointF(800, 500), Color.GreenYellow, null));
                local.Add(new OrbitBall(Color.Yellow, 30, local[0], 0.1, 0));
                local.Add(new OrbitBall(Color.Yellow, 30, local[0], 0.1, Math.PI / 2));
                local.Add(new OrbitBall(Color.Yellow, 30, local[0], 0.1, Math.PI));
                local.Add(new OrbitBall(Color.Yellow, 30, local[0], 0.1, 3 * Math.PI / 2));
                local.Add(new OrbitBall(Color.Yellow, 60, local[0], -0.05, 0));
                local.Add(new OrbitBall(Color.Yellow, 60, local[0], -0.05, Math.PI / 2));
                local.Add(new OrbitBall(Color.Yellow, 60, local[0], -0.05, 3 * Math.PI));
                local.Add(new OrbitBall(Color.Yellow, 60, local[0], -0.05, 3 * Math.PI / 2));
                local.Add(new OrbitBall(Color.Yellow, 90, local[0], 0.025, 0));
                local.Add(new OrbitBall(Color.Yellow, 90, local[0], 0.025, Math.PI / 2));
                local.Add(new OrbitBall(Color.Yellow, 90, local[0], 0.025, Math.PI));
                local.Add(new OrbitBall(Color.Yellow, 90, local[0], 0.025, 3 * Math.PI / 2));
                _shapes.AddRange(local);
            }
            // show animated polygons (interlocking triangles)
            _shapes.Add(new AniPoly(new PointF(100, 300), Color.Tomato, 3, null, 0.1));
            _shapes.Add(new AniPoly(new PointF(135, 300), Color.Tomato, 3, null, -0.1, 1));
            _shapes.Add(new AniPoly(new PointF(170, 300), Color.Tomato, 3, null, 0.1));
            // show string of adjacent relative horizontal wobble balls
            {
                List<Shape> local = new List<Shape>();
                local.Add(new FixedSquare(new PointF(500, 200), Color.Wheat, null));
                for (int i = 1; i < 20; ++i)
                    local.Add(new HWobbleBall(Color.Orange, 25, local[i - 1], 0.1));
                _shapes.AddRange(local);
            }
            // show highlight on a fixed square
            {
                List<Shape> local = new List<Shape>();
                local.Add(new FixedSquare(new PointF(800, 300), Color.LightCoral, null));
                local.Add(new AniHighlight(Color.Yellow, 30, local[0], -0.2));
                _shapes.AddRange(local);
            }
        }
    }
}
