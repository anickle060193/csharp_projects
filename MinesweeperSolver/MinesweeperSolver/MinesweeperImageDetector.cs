using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperSolver
{
    public class MinesweeperImageDetector
    {
        private const int EDGE_DETECTION_THRESHOLD = 40;
        private const int MINIMUM_BLOB_SIZE = 200;

        private static readonly DifferenceEdgeDetector EDGE_DETECTOR = new DifferenceEdgeDetector();
        private static readonly SimpleShapeChecker SHAPE_CHECKER = new SimpleShapeChecker();
        private static readonly Sharpen SHARPEN_FILTER = new Sharpen();

        public static List<List<IntPoint>> FindQuads( Bitmap screenshot, int threshold )
        {
            List<List<IntPoint>> quads = new List<List<IntPoint>>();

            using( Bitmap clone = new Bitmap( screenshot ) )
            {
                Rectangle rect = new Rectangle( 0, 0, clone.Width, clone.Height );
                using( UnmanagedImage image = new UnmanagedImage( clone.LockBits( rect, ImageLockMode.ReadWrite, clone.PixelFormat ) ) )
                {
                    using( UnmanagedImage grayImage = UnmanagedImage.Create( image.Width, image.Height, PixelFormat.Format8bppIndexed ) )
                    {
                        Grayscale.CommonAlgorithms.BT709.Apply( image, grayImage );

                        using( UnmanagedImage edgesImage = EDGE_DETECTOR.Apply( grayImage ) )
                        {
                            Threshold thresholdFilter = new Threshold( threshold );
                            thresholdFilter.ApplyInPlace( edgesImage );

                            BlobCounter blobCounter = new BlobCounter();
                            blobCounter.MinHeight = MINIMUM_BLOB_SIZE;
                            blobCounter.MinWidth = MINIMUM_BLOB_SIZE;
                            blobCounter.FilterBlobs = true;
                            blobCounter.ObjectsOrder = ObjectsOrder.Size;

                            blobCounter.ProcessImage( edgesImage );
                            Blob[] blobs = blobCounter.GetObjectsInformation();

                            foreach( Blob blob in blobs )
                            {
                                List<IntPoint> edgePoints = blobCounter.GetBlobsEdgePoints( blob );
                                List<IntPoint> corners = null;

                                if( SHAPE_CHECKER.IsQuadrilateral( edgePoints, out corners ) )
                                {
                                    quads.Add( corners );
                                }
                            }
                        }
                    }
                }
            }
            return quads;
        }
    }
}
