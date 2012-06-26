using System;
using MonoTouch.CoreAnimation;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using MonoTouch.OpenGLES;
using MonoTouch.UIKit;
using OpenTK;
using OpenTK.Platform.iPhoneOS;
using OpenTK.Graphics.ES20;

namespace OpenGLToy
{
    [Register ("EAGLView")]
    public class EAGLView : iPhoneOSGameView
    {
        [Export("initWithCoder:")]
        public EAGLView(NSCoder coder) : base (coder)
        {
            LayerRetainsBacking = true;
            LayerColorFormat = EAGLColorFormat.RGBA8;
        }
        
        [Export ("layerClass")]
        public static new Class GetLayerClass()
        {
            return iPhoneOSGameView.GetLayerClass();
        }
        
        protected override void ConfigureLayer(CAEAGLLayer eaglLayer)
        {
            eaglLayer.Opaque = true;
        }
        
        protected override void CreateFrameBuffer()
        {
            try
            {
                ContextRenderingApi = EAGLRenderingAPI.OpenGLES2;
                base.CreateFrameBuffer();
            } catch (Exception)
            {
                ContextRenderingApi = EAGLRenderingAPI.OpenGLES1;
                base.CreateFrameBuffer();
            }
            
//            if (ContextRenderingApi == EAGLRenderingAPI.OpenGLES2)
//                LoadShaders ();
        }
        
        protected override void DestroyFrameBuffer()
        {
            base.DestroyFrameBuffer();
//            DestroyShaders ();
        }


        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            
            MakeCurrent();
            
            // Replace the implementation of this method to do your own custom drawing.
            GL.ClearColor(0.5f, 0.25f, 0.25f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            SwapBuffers();
            
        }

                
        #region DisplayLink support
        
        int frameInterval;
        CADisplayLink displayLink;
        
        public bool IsAnimating { get; private set; }
        
        // How many display frames must pass between each time the display link fires.
        public int FrameInterval {
            get {
                return frameInterval;
            }
            set {
                if (value <= 0)
                    throw new ArgumentException ();
                frameInterval = value;
                if (IsAnimating) {
                    StopAnimating ();
                    StartAnimating ();
                }
            }
        }
        
        public void StartAnimating ()
        {
            if (IsAnimating)
                return;
            
            CreateFrameBuffer ();
            CADisplayLink displayLink = UIScreen.MainScreen.CreateDisplayLink (this, new Selector ("drawFrame"));
            displayLink.FrameInterval = frameInterval;
            displayLink.AddToRunLoop (NSRunLoop.Current, NSRunLoop.NSDefaultRunLoopMode);
            this.displayLink = displayLink;
            
            IsAnimating = true;
        }
        
        public void StopAnimating ()
        {
            if (!IsAnimating)
                return;
            displayLink.Invalidate ();
            displayLink = null;
            DestroyFrameBuffer ();
            IsAnimating = false;
        }
        
        [Export ("drawFrame")]
        void DrawFrame ()
        {
            OnRenderFrame (new FrameEventArgs ());
        }
        
        #endregion
        

    }
}

