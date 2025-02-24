﻿using OpenCvSharp.Internal;

namespace OpenCvSharp.XImgProc.Segmentation;

/// <summary>
/// Graph Based Segmentation Algorithm.
/// The class implements the algorithm described in @cite PFF2004.
/// </summary>
public class GraphSegmentation : Algorithm
{
    internal Ptr? PtrObj { get; private set; }

    /// <summary>
    /// Creates instance by raw pointer
    /// </summary>
    protected GraphSegmentation(IntPtr p)
    {
        PtrObj = new Ptr(p);
        ptr = PtrObj.Get();
    }

    /// <summary>
    /// Releases managed resources
    /// </summary>
    protected override void DisposeManaged()
    {
        PtrObj?.Dispose();
        PtrObj = null;
        base.DisposeManaged();
    }

    /// <summary>
    /// Creates a graph based segmentor
    /// </summary>
    /// <param name="sigma">The sigma parameter, used to smooth image</param>
    /// <param name="k">The k parameter of the algorithm</param>
    /// <param name="minSize">The minimum size of segments</param>
    /// <returns></returns>
    public static GraphSegmentation Create(double sigma= 0.5, float k = 300, int minSize = 100)
    {
        NativeMethods.HandleException(
            NativeMethods.ximgproc_segmentation_createGraphSegmentation(sigma, k, minSize, out var p));
        return new GraphSegmentation(p);
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual double Sigma
    {
        get
        {
            ThrowIfDisposed();
            NativeMethods.HandleException(
                NativeMethods.ximgproc_segmentation_GraphSegmentation_getSigma(ptr, out var ret));
            GC.KeepAlive(this);
            return ret;
        }
        set
        {
            ThrowIfDisposed();
            NativeMethods.HandleException(
                NativeMethods.ximgproc_segmentation_GraphSegmentation_setSigma(ptr, value));
            GC.KeepAlive(this);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual float K
    {
        get
        {
            ThrowIfDisposed();
            NativeMethods.HandleException(
                NativeMethods.ximgproc_segmentation_GraphSegmentation_getK(ptr, out var ret));
            GC.KeepAlive(this);
            return ret;
        }
        set
        {
            ThrowIfDisposed();
            NativeMethods.HandleException(
                NativeMethods.ximgproc_segmentation_GraphSegmentation_setK(ptr, value));
            GC.KeepAlive(this);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual int MinSize
    {
        get
        {
            ThrowIfDisposed();
            NativeMethods.HandleException(
                NativeMethods.ximgproc_segmentation_GraphSegmentation_getMinSize(ptr, out var ret));
            GC.KeepAlive(this);
            return ret;
        }
        set
        {
            ThrowIfDisposed();
            NativeMethods.HandleException(
                NativeMethods.ximgproc_segmentation_GraphSegmentation_setMinSize(ptr, value));
            GC.KeepAlive(this);
        }
    }

    /// <summary>
    /// Segment an image and store output in dst
    /// </summary>
    /// <param name="src">The input image. Any number of channel (1 (Eg: Gray), 3 (Eg: RGB), 4 (Eg: RGB-D)) can be provided</param>
    /// <param name="dst">The output segmentation. It's a CV_32SC1 Mat with the same number of cols and rows as input image, with an unique, sequential, id for each pixel.</param>
    public virtual void ProcessImage(InputArray src, OutputArray dst)
    {
        ThrowIfDisposed();
        if (src is null)
            throw new ArgumentNullException(nameof(src));
        if (dst is null)
            throw new ArgumentNullException(nameof(dst));
        src.ThrowIfDisposed();
        dst.ThrowIfDisposed();

        NativeMethods.HandleException(
            NativeMethods.ximgproc_segmentation_GraphSegmentation_processImage(ptr, src.CvPtr, dst.CvPtr));

        GC.KeepAlive(this);
        GC.KeepAlive(src);
        GC.KeepAlive(dst);
    }

    internal class Ptr(IntPtr ptr) : OpenCvSharp.Ptr(ptr)
    {
        public override IntPtr Get()
        {
            NativeMethods.HandleException(
                NativeMethods.ximgproc_segmentation_Ptr_GraphSegmentation_get(ptr, out var ret));
            GC.KeepAlive(this);
            return ret;
        }

        protected override void DisposeUnmanaged()
        {
            NativeMethods.HandleException(
                NativeMethods.ximgproc_segmentation_Ptr_GraphSegmentation_delete(ptr));
            base.DisposeUnmanaged();
        }
    }
}
