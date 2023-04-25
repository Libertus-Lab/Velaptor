﻿// <copyright file="EllipseShapeTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace VelaptorTests.Graphics;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Numerics;
using FluentAssertions;
using Velaptor.Graphics;
using Xunit;

/// <summary>
/// Tests the <see cref="EllipseShape"/> struct.
/// </summary>
public class EllipseShapeTests
{
    /// <summary>
    /// Provides test data for the <see cref="EllipseShape.IsEmpty"/> method unit test.
    /// </summary>
    /// <returns>The data to use during the test.</returns>
    public static IEnumerable<object[]> IsEmptyTestData()
    {
        yield return new object[]
        {
            Vector2.Zero, // Position
            1f, // Width
            1f, // Height
            Color.Empty, // Color
            false, // IsFilled
            1f, // Border Thickness
            ColorGradient.None, // Gradient Type
            Color.Empty, // Gradient Start
            Color.Empty, // Gradient Stop
            true, // EXPECTED
        };
        yield return new object[]
        {
            new Vector2(44, 44), // Position
            1f, // Width
            1f, // Height
            Color.Empty, // Color
            false, // IsFilled
            1f, // Border Thickness
            ColorGradient.None, // Gradient Type
            Color.Empty, // Gradient Start
            Color.Empty, // Gradient Stop
            false, // EXPECTED
        };
        yield return new object[]
        {
            Vector2.Zero, // Position
            44f, // Width
            1f, // Height
            Color.Empty, // Color
            false, // IsFilled
            1f, // Border Thickness
            ColorGradient.None, // Gradient Type
            Color.Empty, // Gradient Start
            Color.Empty, // Gradient Stop
            false, // EXPECTED
        };
        yield return new object[]
        {
            Vector2.Zero, // Position
            1f, // Width
            44f, // Height
            Color.Empty, // Color
            false, // IsFilled
            1f, // Border Thickness
            ColorGradient.None, // Gradient Type
            Color.Empty, // Gradient Start
            Color.Empty, // Gradient Stop
            false, // EXPECTED
        };
        yield return new object[]
        {
            Vector2.Zero, // Position
            1f, // Width
            1f, // Height
            Color.FromArgb(44, 44, 44, 44), // Color
            false, // IsFilled
            1f, // Border Thickness
            ColorGradient.None, // Gradient Type
            Color.Empty, // Gradient Start
            Color.Empty, // Gradient Stop
            false, // EXPECTED
        };
        yield return new object[]
        {
            Vector2.Zero, // Position
            1f, // Width
            1f, // Height
            Color.Empty, // Color
            true, // IsFilled
            1f, // Border Thickness
            ColorGradient.None, // Gradient Type
            Color.Empty, // Gradient Start
            Color.Empty, // Gradient Stop
            false, // EXPECTED
        };
        yield return new object[]
        {
            Vector2.Zero, // Position
            1f, // Width
            1f, // Height
            Color.Empty, // Color
            false, // IsFilled
            44f, // Border Thickness
            ColorGradient.None, // Gradient Type
            Color.Empty, // Gradient Start
            Color.Empty, // Gradient Stop
            false, // EXPECTED
        };
        yield return new object[]
        {
            Vector2.Zero, // Position
            1f, // Width
            1f, // Height
            Color.Empty, // Color
            false, // IsFilled
            1f, // Border Thickness
            ColorGradient.Horizontal, // Gradient Type
            Color.Empty, // Gradient Start
            Color.Empty, // Gradient Stop
            false, // EXPECTED
        };
        yield return new object[]
        {
            Vector2.Zero, // Position
            1f, // Width
            1f, // Height
            Color.Empty, // Color
            false, // IsFilled
            1f, // Border Thickness
            ColorGradient.None, // Gradient Type
            Color.FromArgb(44, 44, 44, 44), // Gradient Start
            Color.Empty, // Gradient Stop
            false, // EXPECTED
        };
        yield return new object[]
        {
            Vector2.Zero, // Position
            1f, // Width
            1f, // Height
            Color.Empty, // Color
            false, // IsFilled
            1f, // Border Thickness
            ColorGradient.None, // Gradient Type
            Color.Empty, // Gradient Start
            Color.FromArgb(44, 44, 44, 44), // Gradient Stop
            false, // EXPECTED
        };
        yield return new object[]
        {
            Vector2.Zero, // Position
            1f, // Width
            1f, // Height
            Color.Empty, // Color
            false, // IsFilled
            1f, // Border Thickness
            ColorGradient.None, // Gradient Type
            Color.Empty, // Gradient Start
            Color.Empty, // Gradient Stop
            true, // EXPECTED
        };
    }

    #region Constructor Tests
    [Fact]
    [SuppressMessage(
        "StyleCop.CSharp.ReadabilityRules",
        "SA1129:Do not use default value type constructor",
        Justification = "Unit test requires use of constructor.")]
    public void Ctor_WhenInvoked_SetsDefaultValues()
    {
        // Arrange & Act
        var sut = new EllipseShape();

        // Assert
        sut.IsFilled.Should().BeTrue();
        sut.Position.Should().Be(Vector2.Zero);
        sut.Width.Should().Be(1f);
        sut.Height.Should().Be(1f);
        sut.Color.Should().Be(Color.White);
        sut.BorderThickness.Should().Be(1f);
        sut.GradientType.Should().Be(ColorGradient.None);
        sut.GradientStart.Should().Be(Color.White);
        sut.GradientStop.Should().Be(Color.White);
    }
    #endregion

    #region Prop Tests
    [Theory]
    [InlineData(0, 1)]
    [InlineData(-10f, 1)]
    [InlineData(123, 123)]
    public void Width_WhenSettingValue_ReturnsCorrectResult(float value, float expected)
    {
        // Arrange
        var sut = default(EllipseShape);

        // Act
        sut.Width = value;
        var actual = sut.Width;

        // Assert
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(-10f, 1)]
    [InlineData(123, 123)]
    public void Height_WhenSettingValue_ReturnsCorrectResult(float value, float expected)
    {
        // Arrange
        var sut = default(EllipseShape);

        // Act
        sut.Height = value;
        var actual = sut.Height;

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void HalfWidth_WhenGettingValue_ReturnsCorrectResult()
    {
        // Arrange
        var sut = default(EllipseShape);
        sut.Width = 100;

        // Act
        var actual = sut.HalfWidth;

        // Assert
        actual.Should().Be(50f);
    }

    [Fact]
    public void HalfHeight_WhenGettingValue_ReturnsCorrectResult()
    {
        // Arrange
        var sut = default(EllipseShape);
        sut.Height = 100;

        // Act
        var actual = sut.HalfHeight;

        // Assert
        actual.Should().Be(50f);
    }

    [Fact]
    public void BorderThickness_WhenSettingValue_ReturnsCorrectResult()
    {
        // Arrange
        var sut = default(EllipseShape);

        // Act
        sut.BorderThickness = 123f;
        var actual = sut.BorderThickness;

        // Assert
        actual.Should().Be(123f);
    }

    [Fact]
    public void Top_WhenSettingValue_ReturnsCorrectResult()
    {
        // Arrange
        var sut = default(EllipseShape);
        sut.Position = new Vector2(100, 100);
        sut.Width = 100;
        sut.Height = 50;

        // Act
        sut.Top = 40f;
        var actual = sut.Top;

        // Assert
        actual.Should().Be(40f);
        sut.Position.X.Should().Be(100);
        sut.Position.Y.Should().Be(65f);
    }

    [Fact]
    public void Right_WhenSettingValue_ReturnsCorrectResult()
    {
        // Arrange
        var sut = default(EllipseShape);
        sut.Position = new Vector2(200, 100);
        sut.Width = 100;
        sut.Height = 50;

        // Act
        sut.Right = 100f;
        var actual = sut.Right;

        // Assert
        actual.Should().Be(100f);
        sut.Position.X.Should().Be(50);
        sut.Position.Y.Should().Be(100f);
    }

    [Fact]
    public void Bottom_WhenSettingValue_ReturnsCorrectResult()
    {
        // Arrange
        var sut = default(EllipseShape);
        sut.Position = new Vector2(100, 100);
        sut.Width = 100;
        sut.Height = 50;

        // Act
        sut.Bottom = 40f;
        var actual = sut.Bottom;

        // Assert
        actual.Should().Be(40f);
        sut.Position.X.Should().Be(100);
        sut.Position.Y.Should().Be(15f);
    }

    [Fact]
    public void Left_WhenSettingValue_ReturnsCorrectResult()
    {
        // Arrange
        var sut = default(EllipseShape);
        sut.Position = new Vector2(200, 100);
        sut.Width = 100;
        sut.Height = 50;

        // Act
        sut.Left = 100f;
        var actual = sut.Left;

        // Assert
        actual.Should().Be(100f);
        sut.Position.X.Should().Be(150);
        sut.Position.Y.Should().Be(100f);
    }
    #endregion

    #region Method Tests
    [Theory]
    [MemberData(nameof(IsEmptyTestData))]
    public void IsEmpty_WhenInvoked_ReturnsCorrectResult(
        Vector2 position,
        float width,
        float height,
        Color color,
        bool isFilled,
        float borderThickness,
        ColorGradient gradientType,
        Color gradientStart,
        Color gradientStop,
        bool expected)
    {
        // Arrange
        var sut = default(EllipseShape);
        sut.Position = position;
        sut.Width = width;
        sut.Height = height;
        sut.Color = color;
        sut.IsFilled = isFilled;
        sut.BorderThickness = borderThickness;
        sut.GradientType = gradientType;
        sut.GradientStart = gradientStart;
        sut.GradientStop = gradientStop;

        // Act
        var actual = sut.IsEmpty();

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Empty_WhenInvoked_EmptiesStruct()
    {
        // Arrange
        var sut = default(EllipseShape);
        sut.Position = new Vector2(1, 2);
        sut.Width = 3f;
        sut.Height = 4f;
        sut.Color = Color.FromArgb(5, 6, 7, 8);
        sut.IsFilled = true;
        sut.BorderThickness = 9f;
        sut.GradientType = ColorGradient.Horizontal;
        sut.GradientStart = Color.FromArgb(14, 15, 16, 17);
        sut.GradientStop = Color.FromArgb(18, 19, 20, 21);

        // Act
        sut.Empty();

        // Assert
        sut.IsFilled.Should().BeFalse();
        sut.Position.Should().Be(Vector2.Zero);
        sut.Width.Should().Be(1f);
        sut.Height.Should().Be(1f);
        sut.Color.Should().Be(Color.Empty);
        sut.BorderThickness.Should().Be(0f);
        sut.GradientType.Should().Be(ColorGradient.None);
        sut.GradientStart.Should().Be(Color.Empty);
        sut.GradientStop.Should().Be(Color.Empty);
    }
    #endregion
}
