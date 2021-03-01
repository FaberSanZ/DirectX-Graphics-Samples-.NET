﻿// Copyright (c) 2019-2020 Faber Leonardo. All Rights Reserved.

/*=============================================================================
	Game.cs
=============================================================================*/

using System;
using System.Collections.Generic;
using System.Text;
using Desktop;
using Renderer;

namespace ClearScreen
{
    public class Game : IDisposable
    {
        public Window Window { get; set; }

        public RenderDescriptor Parameters { get; set; }

        public GraphicsAdapter Adapter { get; set; }

        public GraphicsDevice Device { get; set; }

        public SwapChain SwapChain { get; set; }

        public CommandList CommandList { get; set; }





        public Game()
        {
            Window = new Window("Renderer DirectX 12 - (Clear Screen)", 1000, 720)
            {
                StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen,
            };


            Parameters = new RenderDescriptor()
            {
                BackBufferWidth = Window.Width,
                BackBufferHeight = Window.Height,
                DeviceHandle = Window.Handle,
                Settings = new Settings()
                {
                    Fullscreen = false,
                    VSync = false,
                },
            };
        }

        public void Initialize()
        {

            Adapter = new GraphicsAdapter();

            Device = new GraphicsDevice(Adapter, Parameters);

            SwapChain = new SwapChain(Device);

            CommandList = new CommandList(Device);
        }


        public void Run()
        {
            Initialize();

            BeginRun();

            Window?.Show();

            Tick();
        }

        public void Tick()
        {

            Window.RenderLoop(() =>
            {
                Update();
                Draw();
            });
        }



        public void BeginRun()
        {
            foreach (var Description in Device.NativeAdapter.Description)
                Console.WriteLine(Description);

            foreach (var VendorId in Device.NativeAdapter.VendorId)
                Console.WriteLine(VendorId);

        }

        public void Update()
        {

        }

        public void Draw()
        {
            CommandList.Reset();

            CommandList.SetViewport(0, 0, 800, 600);
            CommandList.SetScissor(0, 0, 800, 600);
            CommandList.ClearTargetColor(SwapChain.BackBuffer, 0.0f, 0.2f, 0.4f, 1.0f);

            CommandList.ExecuteCommandList();

            SwapChain.Present();

        }

        public void Dispose()
        {
            //Device.Dispose();
        }

    }
}
