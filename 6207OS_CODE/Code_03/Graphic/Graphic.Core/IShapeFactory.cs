namespace Graphic.Core
{
public interface IShapeFactory
{
    ICircle CreateCircle();

    ISquare CreateSquare();
}
}