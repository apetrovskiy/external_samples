using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphic.Core
{
public class ShapeService
{
    private readonly IShapeFactory factory;

    public ShapeService(IShapeFactory factory)
    {
        this.factory = factory;
    }

    public void AddShapes(int circles, int squares, ICanvas canvas)
    {
        for (int i = 0; i < circles; i++)
        {
            ICircle circle = factory.CreateCircle();
            canvas.AddShape(circle);
        }
        for (int i = 0; i < squares; i++)
        {
            ISquare square = factory.CreateSquare();
            canvas.AddShape(square);
        }
    }
}
}
