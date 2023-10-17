using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using devDept.Eyeshot.Entities;
using devDept.Geometry;

namespace SampleProject
{
    class Sample : devDept.Eyeshot.Design
    {
        public List<Region> listOfReg = new List<Region>();

        public List<PlanarSurface> listOFPlanes = new List<PlanarSurface>();

        public List<PlanarSurface> TemplistOFPlanes = new List<PlanarSurface>();
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {

            }
        }
        public void CreateAutoPitchSlopedRoof()
        {
            List<Point3D> HardCodedLIstOfPt = new List<Point3D>()
            {
                new Point3D(-2461.54, -530.769, 0),
                new Point3D(-61.5385, 2500, 0),
                new Point3D(2138.46, -423.077, 0),
                new Point3D(-46.1538, 1100, 0)
            };
            List<Line> roofBorder = CreateLineList(HardCodedLIstOfPt);
            Region regionOfHardCoded = Region.CreatePolygon(HardCodedLIstOfPt.ToArray());
            List<Line> autoPitchLines = new List<Line>();

            //In here we are iterating for all the border lines of roof area and creating planar surfaces
            foreach (Line borderLine in roofBorder)
            {
                Console.WriteLine(borderLine.Length());
                double distance = regionOfHardCoded.BoxMin.DistanceTo(regionOfHardCoded.BoxMax);

                /*Point3D point = new Point3D(borderLine.StartPoint.X,);*/
                Vector3D dirForPoint = FindPerpendicularDirectionVector(borderLine.Direction);

                Point3D pointLeft = borderLine.StartPoint + dirForPoint * 100;
                Point3D pointRight = borderLine.EndPoint + dirForPoint * 100;
                bool insideTheRegion = regionOfHardCoded.IsPointInside(pointLeft);
                bool insideTheRegion2 = regionOfHardCoded.IsPointInside(pointRight);

                // Here we are reversing the direction of points for creating planes all inclined toward the roof region
                if (insideTheRegion == false || insideTheRegion2 == false)
                {
                    pointLeft = borderLine.StartPoint + dirForPoint * -1 * 100;
                    pointRight = borderLine.EndPoint + dirForPoint * -1 * 100;
                }


                pointLeft.Z = pointLeft.Z + 100;
                pointRight.Z = pointRight.Z + 100;

                Line line = new Line(borderLine.StartPoint, pointLeft);

                Vector3D vectorDir = line.Direction;
                vectorDir.Normalize();

                Vector3D vectorDir2 = borderLine.Direction;
                vectorDir2.Normalize();

                Point3D pointLeftForReg = (borderLine.StartPoint + vectorDir * distance) - vectorDir2 * distance * 2;
                Point3D pointRightForReg = (borderLine.EndPoint + vectorDir * distance) + vectorDir2 * distance * 2/*distance / 4*/; ;

                Line line1 = new Line(pointLeftForReg, pointRightForReg);
                Console.WriteLine(line1.Length());

                // Point3D[] array = { borderLine.StartPoint, borderLine.EndPoint, pointLeftForReg, pointRightForReg };
                // Region reg = Region.CreatePolygon(array);
                List<Point3D> listOfPoint = new List<Point3D>();
                listOfPoint.Add(borderLine.StartPoint);
                listOfPoint.Add(pointLeftForReg);
                listOfPoint.Add(pointRightForReg);
                listOfPoint.Add(borderLine.EndPoint);


                Region reg = Region.CreatePolygon(listOfPoint.ToArray());
                PlanarSurface srf1 = reg.ConvertToSurface();
                regionOfHardCoded.Regen(1e-3);

                listOfReg.Add(reg);
                listOFPlanes.Add(srf1);
                TemplistOFPlanes.Add((PlanarSurface)srf1.Clone());
                /*Plane planeForBorderLine = new Plane(borderLine.StartPoint, borderLine.EndPoint, point);
                listOfPlanes.Add(planeForBorderLine);*/
                //MainWindowController.GetInstance()._views._3DDesign.Entities.Add(planeForBorderLine);


            }

            List<PlanarSurface> templistOFPlanes = new List<PlanarSurface>();
            for (int i = 0; i < listOFPlanes.Count; i++)
            {
                templistOFPlanes.Add((PlanarSurface)listOFPlanes[i].Clone());
            }

            const double wantedTol = 1e-3;

            //In here for the first iteration we are triming each planar surfaces with its immidiate neighbours
            for (int i = 0; i < listOFPlanes.Count; i++)
            {
                int index1 = -1, index2 = -1;
                if (i == 0)
                {
                    index1 = listOFPlanes.Count - 1;
                    index2 = i + 1;
                }
                else if (i == listOFPlanes.Count - 1)
                {
                    index1 = i - 1;
                    index2 = 0;
                }
                else if (i > 0)
                {
                    index1 = i - 1;
                    index2 = i + 1;
                }
                else
                {
                    break;
                }

                PlanarSurface CloneOfPlanarSurface = (PlanarSurface)listOFPlanes[i].Clone();

                ssiFailureType res1 = listOFPlanes[i].TrimBy(templistOFPlanes[index1], wantedTol, false);

                if (res1 == ssiFailureType.Success)
                {
                    bool status = KnowIfTheResultOfTrimIsCorrectOrNot(listOFPlanes[i], roofBorder[i]);
                    if (status == false)
                    {
                        listOFPlanes[i] = (PlanarSurface)CloneOfPlanarSurface.Clone();
                        ssiFailureType res2 = listOFPlanes[i].TrimBy(templistOFPlanes[index1], wantedTol, true);
                        if (res2 == ssiFailureType.Success)
                        {
                            status = KnowIfTheResultOfTrimIsCorrectOrNot(listOFPlanes[i], roofBorder[i]);
                            if (status == false)
                            {
                                listOFPlanes[i] = CloneOfPlanarSurface;
                            }
                        }
                        else
                        {
                            int a = 0;
                            a++;
                        }
                    }
                }
                else
                {
                    int a = 0;
                    a++;
                }

                CloneOfPlanarSurface = (PlanarSurface)listOFPlanes[i].Clone();

                res1 = listOFPlanes[i].TrimBy(templistOFPlanes[index2], wantedTol, false);

                if (res1 == ssiFailureType.Success)
                {
                    bool status = KnowIfTheResultOfTrimIsCorrectOrNot(listOFPlanes[i], roofBorder[i]);
                    if (status == false)
                    {
                        listOFPlanes[i] = (PlanarSurface)CloneOfPlanarSurface.Clone();
                        ssiFailureType res2 = listOFPlanes[i].TrimBy(templistOFPlanes[index2], wantedTol, true);
                        if (res2 == ssiFailureType.Success)
                        {
                            status = KnowIfTheResultOfTrimIsCorrectOrNot(listOFPlanes[i], roofBorder[i]);
                            if (status == false)
                            {
                                listOFPlanes[i] = CloneOfPlanarSurface;
                            }
                        }
                        else
                        {
                            int a = 0;
                            a++;
                        }
                    }
                }
                else
                {
                    int a = 0;
                    a++;
                }
            }

            templistOFPlanes.Clear();
            for (int i = 0; i < listOFPlanes.Count; i++)
            {
                templistOFPlanes.Add((PlanarSurface)listOFPlanes[i].Clone());
                templistOFPlanes[templistOFPlanes.Count - 1].Regen(1e-3);
            }

            //In here we are trimming each surfaces with all the other surfaces which are intersecting with it except its immidiate neighbours and itself.
            for (int i = 0; i < listOFPlanes.Count; i++)
            {
                List<Surface> intersectedPlanes = new List<Surface>();
                for (int k = 0; k < listOFPlanes.Count; k++)
                {
                    if (i == k)
                        continue;

                    int index1 = -1, index2 = -1;
                    if (i == 0)
                    {
                        index1 = listOFPlanes.Count - 1;
                        index2 = i + 1;
                    }
                    else if (i == listOFPlanes.Count - 1)
                    {
                        index1 = i - 1;
                        index2 = 0;
                    }
                    else if (i > 0)
                    {
                        index1 = i - 1;
                        index2 = i + 1;
                    }

                    if (index1 == k || index2 == k)
                        continue;

                    bool boolval = listOFPlanes[i].Intersects(listOFPlanes[k], wantedTol);
                    if (boolval)
                    {
                        intersectedPlanes.Add((PlanarSurface)listOFPlanes[k].Clone());
                        intersectedPlanes[intersectedPlanes.Count - 1].Regen(1e-3);
                    }
                }

                PlanarSurface CloneOfPlanarSurface = (PlanarSurface)listOFPlanes[i].Clone();
                ssiFailureType resss = listOFPlanes[i].TrimBy(intersectedPlanes, wantedTol, false);
                if (resss == ssiFailureType.Success)
                {
                    bool status = KnowIfTheResultOfTrimIsCorrectOrNot(listOFPlanes[i], roofBorder[i]);
                    if (status == false)
                    {
                        listOFPlanes[i] = CloneOfPlanarSurface;
                        resss = listOFPlanes[i].TrimBy(intersectedPlanes, wantedTol, true);
                        if (resss == ssiFailureType.Success)
                        {
                            status = KnowIfTheResultOfTrimIsCorrectOrNot(listOFPlanes[i], roofBorder[i]);
                            if (status == false)
                            {
                                listOFPlanes[i] = CloneOfPlanarSurface;
                            }
                        }
                    }
                    else
                    {
                        int aaa = 0;
                        aaa++;
                    }
                }
                int a = 0;
                a++;
            }

            for (int j = 0; j < roofBorder.Count; j++)
            {
                Console.WriteLine("\n roofBorder");
                Console.WriteLine("\n Start", roofBorder[j].StartPoint);
                Console.WriteLine(roofBorder[j].StartPoint);
                Console.WriteLine("\n End", roofBorder[j].EndPoint);
                Console.WriteLine(roofBorder[j].EndPoint);
            }
            for (int i = 0; i < listOFPlanes.Count; i++)
            {
                Entities.Add(listOFPlanes[i], System.Drawing.Color.Green);
                Entities.Add(listOFPlanes[i], System.Drawing.Color.Green);
            }

        }
        public static Vector3D FindPerpendicularDirectionVector(Vector3D dir)
        {
            Vector3D dirPerp;
            dirPerp = new Vector3D(-dir.Y, dir.X);
            dirPerp.Normalize();
            return dirPerp;
        }

        public List<Line> CreateLineList(List<Point3D> pointList)
        {
            List<Line> lineList = new List<Line>();
            for (int i = 0; i < pointList.Count; i++)
            {
                if (i == pointList.Count - 1)
                {
                    Line line = new Line(pointList[i], pointList[0]);
                    lineList.Add(line);
                }
                else
                {
                    Line line = new Line(pointList[i], pointList[i + 1]);
                    lineList.Add(line);
                }
            }
            return lineList;
        }
        public bool KnowIfTheResultOfTrimIsCorrectOrNot(PlanarSurface pln, Line line)
        {
            bool isTheCorrectPlane = false;
            pln.Regen(1e-3);
            int count = 0;
            foreach (Point3D planePt in pln.Vertices)
            {
                /*Circle c1 = new Circle(planePt, 80);
                MainWindowController.GetInstance()._views._3DDesign.Entities.Add(c1, System.Drawing.Color.Black);*/
                double distanceToThePlanePoints = line.StartPoint.DistanceTo(planePt);
                double distanceToThePlanePoints2 = line.EndPoint.DistanceTo(planePt);
                if (distanceToThePlanePoints < 1e-3 || distanceToThePlanePoints2 < 1e-3)
                {
                    count = count + 1;
                }
            }

            if (count == 2)
            {
                isTheCorrectPlane = true;
            }

            return isTheCorrectPlane;
        }
    }
}
