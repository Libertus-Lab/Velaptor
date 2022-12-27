// <copyright file="IoC.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace Velaptor;

using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Abstractions;
using Carbonate;
using SimpleInjector;
using Content;
using Content.Caching;
using Content.Factories;
using Content.Fonts.Services;
using Factories;
using Input;
using NativeInterop.FreeType;
using NativeInterop.GLFW;
using NativeInterop.OpenGL;
using OpenGL;
using OpenGL.Services;
using Services;

/// <summary>
/// Provides dependency injection for the application.
/// </summary>
[ExcludeFromCodeCoverage(Justification = $"Cannot test due to direct interaction with the '{nameof(SimpleInjector)}' library.")]
internal static class IoC
{
    private static readonly FileSystem FileSystem = new ();
    private static readonly IFileStreamFactory FileStream = FileSystem.FileStream;
    private static readonly Container IoCContainer = new ();
    private static bool isInitialized;

    /// <summary>
    /// Gets the inversion of control container used to get instances of objects.
    /// </summary>
    public static Container Container
    {
        get
        {
            if (!isInitialized)
            {
                SetupContainer();
            }

            return IoCContainer;
        }
    }

    /// <summary>
    /// Sets up the IoC container.
    /// </summary>
    private static void SetupContainer()
    {
        SetupNativeInterop();

        SetupCaching();

        SetupFactories();

        SetupServices();

        SetupContent();

        IoCContainer.Register<IReactable>(() => new Reactable(), Lifestyle.Singleton);
        IoCContainer.Register<IAppInput<KeyboardState>, Keyboard>(Lifestyle.Singleton);
        IoCContainer.Register<IAppInput<MouseState>, Mouse>(Lifestyle.Singleton);
        IoCContainer.Register<IFontMetaDataParser, FontMetaDataParser>(Lifestyle.Singleton);

        isInitialized = true;
    }

    /// <summary>
    /// Sets up container registration related to OpenGL.
    /// </summary>
    private static void SetupNativeInterop()
    {
        IoCContainer.Register(() => FileSystem.File, Lifestyle.Singleton);
        IoCContainer.Register(() => FileSystem.Directory, Lifestyle.Singleton);
        IoCContainer.Register(() => FileSystem.Path, Lifestyle.Singleton);
        IoCContainer.Register(() => FileStream, Lifestyle.Singleton);
        IoCContainer.Register<IPlatform, Platform>(Lifestyle.Singleton);

        IoCContainer.Register<IGLInvoker, GLInvoker>(Lifestyle.Singleton);
        IoCContainer.Register<IOpenGLService, OpenGLService>(Lifestyle.Singleton);

        IoCContainer.Register<GLFWMonitors>(Lifestyle.Singleton);

        IoCContainer.Register<IGLFWInvoker, GLFWInvoker>(Lifestyle.Singleton);

        IoCContainer.Register<IFreeTypeInvoker, FreeTypeInvoker>(Lifestyle.Singleton);
        IoCContainer.Register<IMonitors, GLFWMonitors>(Lifestyle.Singleton);
    }

    /// <summary>
    /// Sets up container registration related to caching.
    /// </summary>
    private static void SetupCaching()
    {
        IoCContainer.Register<IItemCache<string, ITexture>, TextureCache>(Lifestyle.Singleton);
        IoCContainer.Register<IItemCache<string, ISound>, SoundCache>(Lifestyle.Singleton);
    }

    /// <summary>
    /// Sets up container registration related to factories.
    /// </summary>
    private static void SetupFactories()
    {
        IoCContainer.Register<IWindowFactory, SilkWindowFactory>(Lifestyle.Singleton);
        IoCContainer.Register<INativeInputFactory, NativeInputFactory>(Lifestyle.Singleton);
        IoCContainer.Register<ISoundFactory, SoundFactory>(Lifestyle.Singleton);
        IoCContainer.Register<ITextureFactory, TextureFactory>(Lifestyle.Singleton);
        IoCContainer.Register<IAtlasDataFactory, AtlasDataFactory>(Lifestyle.Singleton);
        IoCContainer.Register<IShaderFactory, ShaderFactory>(Lifestyle.Singleton);
        IoCContainer.Register<IGPUBufferFactory, GPUBufferFactory>(Lifestyle.Singleton);
        IoCContainer.Register<IFontFactory, FontFactory>();
    }

    /// <summary>
    /// Sets up container registration related to services.
    /// </summary>
    private static void SetupServices()
    {
        IoCContainer.Register<IConsoleService, ConsoleService>(Lifestyle.Singleton);
        IoCContainer.Register<IDateTimeService, DateTimeService>(Lifestyle.Singleton);
        IoCContainer.Register<IConsoleLoggerService, ConsoleLoggerService>(Lifestyle.Singleton);
        IoCContainer.Register<IFileLoggerService, FileLoggerService>(Lifestyle.Singleton);
        IoCContainer.Register<IEventLoggerService, EventLoggerService>(Lifestyle.Singleton);
        IoCContainer.Register<ILoggingService, LoggingService>(Lifestyle.Singleton);
        IoCContainer.Register<IAppSettingsService, AppSettingsService>(Lifestyle.Singleton);
        IoCContainer.Register<IImageService, ImageService>(Lifestyle.Singleton);
        IoCContainer.Register<IEmbeddedResourceLoaderService<string>, TextResourceLoaderService>(Lifestyle.Singleton);
        IoCContainer.Register<ITemplateProcessorService, ShaderTemplateProcessorService>(Lifestyle.Singleton);
        IoCContainer.Register<IShaderLoaderService<uint>, TextureShaderResourceLoaderService>(Lifestyle.Singleton);
        IoCContainer.Register<ISystemMonitorService, SystemMonitorService>(Lifestyle.Singleton);
        IoCContainer.Register<IFontAtlasService, FontAtlasService>(Lifestyle.Singleton);
        IoCContainer.Register<IJSONService, JSONService>(Lifestyle.Singleton);
        IoCContainer.Register<IEmbeddedResourceLoaderService<Stream?>, EmbeddedFontResourceService>(Lifestyle.Singleton);
        IoCContainer.Register<IFontService, FontService>(Lifestyle.Singleton);
        IoCContainer.Register<IBatchingService<TextureBatchItem>, TextureBatchingService>(Lifestyle.Singleton);
        IoCContainer.Register<IBatchingService<FontGlyphBatchItem>, FontGlyphBatchingService>(Lifestyle.Singleton);
        IoCContainer.Register<IBatchingService<RectBatchItem>, RectBatchingService>(Lifestyle.Singleton);
        IoCContainer.Register<IBatchingService<LineBatchItem>, LineBatchingService>(Lifestyle.Singleton);
        IoCContainer.Register<IBatchServiceManager, BatchServiceManager>(Lifestyle.Singleton);

        IoCContainer.Register<IFontStatsService>(
            () => new FontStatsService(
                IoCContainer.GetInstance<IFontService>(),
                PathResolverFactory.CreateFontPathResolver(),
                PathResolverFactory.CreateSystemFontPathResolver(),
                IoCContainer.GetInstance<IDirectory>(),
                IoCContainer.GetInstance<IPath>()), Lifestyle.Singleton);

        IoCContainer.Register<ITaskService, TaskService>();
        IoCContainer.SuppressDisposableTransientWarning<ITaskService>();
    }

    /// <summary>
    /// Sets up container registration related to content.
    /// </summary>
    private static void SetupContent() => IoCContainer.Register<AtlasTexturePathResolver>();
}
