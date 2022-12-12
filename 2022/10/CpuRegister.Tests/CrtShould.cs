using FluentAssertions;

namespace CpuRegister.Tests;

public class CrtShould
{
    [Theory]
    [InlineData(1, 0, 1, '#')]
    [InlineData(2, 1, 1, '#')]
    [InlineData(3, 2, 16, '.')]
    [InlineData(4, 3, 16, '.')]
    [InlineData(5, 4, 5, '#')]
    [InlineData(6, 5, 5, '#')]
    [InlineData(7, 6, 11, '.')]
    [InlineData(8, 7, 11, '.')]
    [InlineData(9, 8, 8, '#')]
    [InlineData(10, 9, 8, '#')]
    [InlineData(11, 10, 13, '.')]
    [InlineData(12, 11, 13, '.')]
    [InlineData(13, 12, 12, '#')]
    [InlineData(14, 13, 12, '#')]
    [InlineData(15, 14, 4, '.')]
    [InlineData(16, 15, 4, '.')]
    [InlineData(17, 16, 17, '#')]
    [InlineData(18, 17, 17, '#')]
    [InlineData(19, 18, 21, '.')]
    [InlineData(20, 19, 21, '.')]
    [InlineData(21, 20, 21, '#')]
    public void Draw_Pixel_On_First_Line(
        int cycleNumber,
        int pixelPositionX,
        int registerValue,
        char expectedPixel)
    {
        var sut = new Crt();
        sut.Draw(cycleNumber, registerValue);
        sut.Screen[pixelPositionX, 0].Should().Be(expectedPixel);
    }
    
    [Theory]
    [InlineData(40, 39, 0, 38, '#')]
    [InlineData(41, 0, 1, 38, '.')]
    [InlineData(42, 1, 1, 1, '#')]
    [InlineData(80, 39, 1, 1, '.')]
    [InlineData(82, 1, 2, 1, '#')]
    public void Draw_Pixels_On_Different_Lines(
        int cycleNumber,
        int pixelPositionX,
        int pixelPositionY,
        int registerValue,
        char expectedPixel)
    {
        var sut = new Crt();
        sut.Draw(cycleNumber, registerValue);
        sut.Screen[pixelPositionX, pixelPositionY].Should().Be(expectedPixel);
    }
}