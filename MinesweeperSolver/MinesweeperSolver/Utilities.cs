using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinesweeperSolver
{
    public static class Utilities
    {
        public static Point[] ConvertToPoint( List<AForge.IntPoint> points )
        {
            return points.ConvertAll( p => new Point( p.X, p.Y ) ).ToArray();
        }

        public static Point[] ConvertToPoint( AForge.IntPoint[] points )
        {
            return Utilities.ConvertToPoint( points );
        }

        public static float GetImageScale( PictureBox pixBox )
        {
            if( pixBox.Image == null )
            {
                return 1.0f;
            }
            float imageWidth = pixBox.Image.Width;
            float imageHeight = pixBox.Image.Height;
            float pixBoxWidth = pixBox.ClientSize.Width;
            float pixBoxHeight = pixBox.ClientSize.Height;
            return Math.Max( imageWidth / pixBoxWidth, imageHeight / pixBoxHeight );
        }

        public static Point[] ConvertToImagePoints( PictureBox pic, Point[] controlPoints )
        {
            return controlPoints.ToList().ConvertAll( p => ConvertToImagePoint( pic, p ) ).ToArray();
        }

        public static Point ConvertToImagePoint( PictureBox pic, Point controlPoint )
        {
            int boxWidth = pic.ClientSize.Width;
            int boxHeight = pic.ClientSize.Height;
            int imageWidth = pic.Image.Width;
            int imageHeight = pic.Image.Height;

            int scaledX = controlPoint.X;
            int scaledY = controlPoint.Y;
            switch( pic.SizeMode )
            {
                case PictureBoxSizeMode.AutoSize:
                case PictureBoxSizeMode.Normal:
                    // These are okay. Leave them alone.
                    break;

                case PictureBoxSizeMode.CenterImage:
                    scaledX = controlPoint.X - ( boxWidth - imageWidth ) / 2;
                    scaledY = controlPoint.Y - ( boxHeight - imageHeight ) / 2;
                    break;

                case PictureBoxSizeMode.StretchImage:
                    scaledX = (int)( imageWidth * controlPoint.X / (float)boxWidth );
                    scaledY = (int)( imageHeight * controlPoint.Y / (float)boxHeight );
                    break;

                case PictureBoxSizeMode.Zoom:
                    float pic_aspect = boxWidth / (float)boxHeight;
                    float img_aspect = imageWidth / (float)imageHeight;
                    if( pic_aspect > img_aspect )
                    {
                        // The PictureBox is wider/shorter than the image.
                        scaledY = (int)( imageHeight * controlPoint.Y / (float)boxHeight );

                        // The image fills the height of the PictureBox.
                        // Get its width.
                        float scaled_width = imageWidth * boxHeight / imageHeight;
                        float dx = ( boxWidth - scaled_width ) / 2;
                        scaledX = (int)( ( controlPoint.X - dx ) * imageHeight / (float)boxHeight );
                    }
                    else
                    {
                        // The PictureBox is taller/thinner than the image.
                        scaledX = (int)( imageWidth * controlPoint.X / (float)boxWidth );

                        // The image fills the height of the PictureBox.
                        // Get its height.
                        float scaled_height = imageHeight * boxWidth / imageWidth;
                        float dy = ( boxHeight - scaled_height ) / 2;
                        scaledY = (int)( ( controlPoint.Y - dy ) * imageWidth / boxWidth );
                    }
                    break;
            }
            return new Point( scaledX, scaledY );
        }

        public static Point[] ConvertToControlPoints( PictureBox pic, Point[] imagePoints )
        {
            return imagePoints.ToList().ConvertAll( p => ConvertToControlPoint( pic, p ) ).ToArray();
        }

        public static Point ConvertToControlPoint( PictureBox box, Point imagePoint )
        {
            int boxWidth = box.ClientSize.Width;
            int boxHeight = box.ClientSize.Height;
            int imageWidth = box.Image.Width;
            int imageHeight = box.Image.Height;

            int scaledX = imagePoint.X;
            int scaledY = imagePoint.Y;
            switch( box.SizeMode )
            {
                case PictureBoxSizeMode.AutoSize:
                case PictureBoxSizeMode.Normal:
                    // These are okay. Leave them alone.
                    break;

                case PictureBoxSizeMode.CenterImage:
                    scaledX = imagePoint.X + ( boxWidth - imageWidth ) / 2;
                    scaledY = imagePoint.Y + ( boxHeight - imageHeight ) / 2;
                    break;

                case PictureBoxSizeMode.StretchImage:
                    scaledX = (int)( imagePoint.X * boxWidth / (float)imageWidth );
                    scaledY = (int)( imagePoint.Y * boxHeight / (float)imageHeight );
                    break;

                case PictureBoxSizeMode.Zoom:
                    float pic_aspect = boxWidth / (float)boxHeight;
                    float img_aspect = imageWidth / (float)imageHeight;
                    if( pic_aspect > img_aspect )
                    {
                        // The PictureBox is wider/shorter than the image.
                        scaledY = (int)( imagePoint.Y * boxHeight / (float)imageHeight );

                        // The image fills the height of the PictureBox.
                        // Get its width.
                        float scaled_width = imageWidth * boxHeight / (float)imageHeight;
                        float dx = ( boxWidth - scaled_width ) / 2;
                        scaledX = (int)( imagePoint.X * boxHeight / (float)imageHeight + dx );
                    }
                    else
                    {
                        // The PictureBox is taller/thinner than the image.
                        scaledX = (int)( imagePoint.X * boxWidth / (float)imageWidth );

                        // The image fills the height of the PictureBox.
                        // Get its height.
                        float scaled_height = imageHeight * boxWidth / (float)imageWidth;
                        float dy = ( boxHeight - scaled_height ) / 2;
                        scaledY = (int)( imagePoint.Y * boxWidth / (float)imageWidth + dy );
                    }
                    break;
            }
            return new Point( scaledX, scaledY );
        }
    }
}
