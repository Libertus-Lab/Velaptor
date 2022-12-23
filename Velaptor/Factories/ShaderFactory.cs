// <copyright file="ShaderFactory.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace Velaptor.Factories;

using System.Diagnostics.CodeAnalysis;
using Carbonate;
using Velaptor.NativeInterop.OpenGL;
using Velaptor.OpenGL.Services;
using OpenGL.Shaders;
using Reactables.Core;
using Reactables.ReactableData;

/// <summary>
/// Creates instance of type <see cref="IShaderProgram"/>.
/// </summary>
[ExcludeFromCodeCoverage]
internal sealed class ShaderFactory : IShaderFactory
{
    private static IShaderProgram? textureShader;
    private static IShaderProgram? fontShader;
    private static IShaderProgram? rectShader;
    private static IShaderProgram? lineShader;

    /// <inheritdoc/>
    public IShaderProgram CreateTextureShader()
    {
        if (textureShader is not null)
        {
            return textureShader;
        }

        var glInvoker = IoC.Container.GetInstance<IGLInvoker>();
        var glInvokerExtensions = IoC.Container.GetInstance<IOpenGLService>();
        var shaderLoaderService = IoC.Container.GetInstance<IShaderLoaderService<uint>>();
        var reactable = IoC.Container.GetInstance<IReactable>();
        var shutDownReactable = IoC.Container.GetInstance<IReactable<ShutDownData>>();

        textureShader = new TextureShader(
            glInvoker,
            glInvokerExtensions,
            shaderLoaderService,
            reactable,
            shutDownReactable);

        return textureShader;
    }

    /// <inheritdoc/>
    public IShaderProgram CreateFontShader()
    {
        if (fontShader is not null)
        {
            return fontShader;
        }

        var glInvoker = IoC.Container.GetInstance<IGLInvoker>();
        var glInvokerExtensions = IoC.Container.GetInstance<IOpenGLService>();
        var shaderLoaderService = IoC.Container.GetInstance<IShaderLoaderService<uint>>();
        var reactable = IoC.Container.GetInstance<IReactable>();
        var shutDownReactable = IoC.Container.GetInstance<IReactable<ShutDownData>>();

        fontShader = new FontShader(
            glInvoker,
            glInvokerExtensions,
            shaderLoaderService,
            reactable,
            shutDownReactable);

        return fontShader;
    }

    /// <inheritdoc/>
    public IShaderProgram CreateRectShader()
    {
        if (rectShader is not null)
        {
            return rectShader;
        }

        var glInvoker = IoC.Container.GetInstance<IGLInvoker>();
        var glInvokerExtensions = IoC.Container.GetInstance<IOpenGLService>();
        var shaderLoaderService = IoC.Container.GetInstance<IShaderLoaderService<uint>>();
        var reactable = IoC.Container.GetInstance<IReactable>();
        var shutDownReactable = IoC.Container.GetInstance<IReactable<ShutDownData>>();

        rectShader = new RectangleShader(
            glInvoker,
            glInvokerExtensions,
            shaderLoaderService,
            reactable,
            shutDownReactable);

        return rectShader;
    }

    /// <inheritdoc/>
    public IShaderProgram CreateLineShader()
    {
        if (lineShader is not null)
        {
            return lineShader;
        }

        var glInvoker = IoC.Container.GetInstance<IGLInvoker>();
        var glInvokerExtensions = IoC.Container.GetInstance<IOpenGLService>();
        var shaderLoaderService = IoC.Container.GetInstance<IShaderLoaderService<uint>>();
        var reactable = IoC.Container.GetInstance<IReactable>();
        var shutDownReactable = IoC.Container.GetInstance<IReactable<ShutDownData>>();

        lineShader = new LineShader(
            glInvoker,
            glInvokerExtensions,
            shaderLoaderService,
            reactable,
            shutDownReactable);

        return lineShader;
    }
}
