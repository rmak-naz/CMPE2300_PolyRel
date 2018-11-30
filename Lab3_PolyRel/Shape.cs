using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GDIDrawer;

namespace Lab3_PolyRel
{
    public interface IRender
    {
        // render instance to the supplied drawer
        void Render(CDrawer dr);
    }

    public interface IAnimate
    {
        // cause per-tick state changes to instance(movement, animation, etc...)
        void Tick();
    }

    abstract class Shape : IRender
    {
        protected PointF _pos;
        protected Color _color;
        protected Shape _parentShape = null;

        public Shape(PointF pos, Color color)
        {
            _pos = pos;
            _color = color;            
        }

        public interface IRender
        {
            // render instance to the supplied drawer
            void Render(CDrawer dr);
        }

        public virtual void Render(CDrawer dr)
        {
            if (_parentShape != null)
            {
                dr.AddLine((int)_pos.X, (int)_pos.Y, (int)_parentShape._pos.X, (int)_parentShape._pos.Y, Color.White);
            }
        }
    }

    class FixedSquare : Shape
    {
        public FixedSquare(PointF pos, Color color, Shape parent) 
            : base(pos, color)
        {
            _parentShape = parent;
        }

        public override void Render(CDrawer dr)            
        {
            base.Render(dr);
            dr.AddRectangle((int)_pos.X, (int)_pos.Y, 20, 20, _color);
        }
    }

    abstract class AniShape : Shape, IAnimate
    {
        protected double _sequenceVal;
        protected double _sequenceDelta;

        public AniShape(double offset, double delta, PointF pos, Color color) 
            : base(pos, color)
        {
            _sequenceVal = offset;
            _sequenceDelta = delta;
        }

        public virtual void Tick()
        {
            _sequenceVal += _sequenceDelta;
        }
    }

    class AniPoly : AniShape
    {
        int _sides;

        public AniPoly(double offset, double delta, int sides, PointF pos, Color color) 
            : base(offset, delta, pos, color)
        {
            if (sides < 3)
                throw new ArgumentException("Can't have a shape with less than 3 sides.");
            else
                _sides = sides;
        }

        public override void Render(CDrawer dr)
        {            
            dr.AddPolygon((int)_pos.X, (int)_pos.Y, 25, _sides, _sequenceVal, _color);
        }
    }

    abstract class AniChild : AniShape
    {
        protected double distance;

        public AniChild(double offset, double delta, Shape parent, PointF pos, Color color) 
            : base(offset, delta, pos, color)
        {
            if (parent == null)
                throw new ArgumentException("Parent can't be null");
            else
                _parentShape = parent;

            base.Tick();
            distance = Math.Sqrt(Math.Pow(_pos.X, 2) + Math.Pow(_pos.Y, 2));
        }
    }

    class AniHighlight : AniChild
    {
        public AniHighlight(double offset, double delta, Shape parent, PointF pos, Color color) 
            : base(offset, delta, parent, pos, color)
        {
        }

        public override void Render(CDrawer dr)
        {
            base.Render(dr);
            //dr.AddBezier((int)_pos.X, (int)_pos.Y, (int)_pos.X, (int)_pos.Y, (int)_pos.X +)
        }
    }

    abstract class AniBall : AniChild
    {
        public AniBall(double offset, double delta, Shape parent, PointF pos, Color color) 
            : base(offset, delta, parent, pos, color)
        {
        }

        public override void Render(CDrawer dr)
        {
            dr.AddCenteredEllipse((int)_pos.X, (int)_pos.Y, 20, 20, _color);
        }
    }

    class OrbitBall : AniBall
    {
        public OrbitBall(Color color, double offset, Shape parent, double delta, PointF pos ) 
            : base(offset, delta, parent, pos, color)
        {
        }

        public override void Tick()
        {
            base.Tick();
        }
    }

    class VWobbleBall : AniBall
    {
        public VWobbleBall(double offset, double delta, Shape parent, PointF pos, Color color) 
            : base(offset, delta, parent, pos, color)
        {
        }

        public override void Tick()
        {
            base.Tick();
        }
    }

    class HWobbleBall : AniBall
    {
        public HWobbleBall(double offset, double delta, Shape parent, PointF pos, Color color) 
            : base(offset, delta, parent, pos, color)
        {
        }

        public override void Tick()
        {
            base.Tick();
        }
    }

}
