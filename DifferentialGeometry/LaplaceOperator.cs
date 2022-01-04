﻿using System.Collections.Generic;
using System.Linq;
using Plankton;

namespace CoralGeometry
{
    public class LaplaceOperator : DifferentialGeometry
    {
        /// <summary>
        /// Compute the uniform laplace position of each vertex.
        /// </summary>
        /// <param name="pmesh">Input a plankton mesh.</param>
        /// <returns>The uniform laplace position of each vertex.</returns>
        public static Vector3D[] UniformLaplace(PlanktonMesh pmesh)
        {
            Vector3D[] laplace = new Vector3D[pmesh.Vertices.Count];
            for (int i = 0; i < pmesh.Halfedges.Count; i++)
            {
                if (!(pmesh.Halfedges[i].AdjacentFace == -1))
                {
                    var a = pmesh.Halfedges[i].StartVertex;
                    var b = pmesh.Halfedges.EndVertex(i);
                    laplace[a] += pmesh.Vertices[b].ToVector3D();
                }
            }

            for (int i = 0; i < laplace.Length; i++)
                laplace[i] /= pmesh.Vertices.GetValence(i);

            return laplace;
        }

        /// <summary>
        /// Compute the cotangent laplace position of each vertex.
        /// </summary>
        /// <param name="pmesh">Input a plankton mesh.</param>
        /// <returns>The cotangent laplace positions of each vertex.</returns>
        public static Vector3D[] CotangentLaplace(PlanktonMesh pmesh)
        {
            var laplace = new Vector3D[pmesh.Vertices.Count];
            var ew = CotLaplaceEdgeWeight(pmesh);
            for (int i = 0; i < pmesh.Vertices.Count; i++)
            {
                laplace[i] = Vector3D.Origin;
                if (!pmesh.Vertices.IsBoundary(i))
                {
                    double w = 0;
                    var hes = pmesh.Vertices.GetHalfedges(i);
                    for (int j = 0; j < hes.Length; j++)
                    {
                        w += ew[hes[j] / 2];
                        laplace[i] +=
                            (pmesh.Vertices[pmesh.Halfedges.EndVertex(hes[j])].ToVector3D() -
                             pmesh.Vertices[i].ToVector3D()) * ew[hes[j] / 2];
                    }

                    laplace[i] /= w;
                }
            }

            return laplace;
        }

        /// <summary>
        /// Compute the cotangent laplace weight of each edge.
        /// </summary>
        /// <param name="pmesh">Input a plankton mesh</param>
        /// <returns>Return the cotangent laplace weight of each edge.</returns>
        public static double[] CotLaplaceEdgeWeight(PlanktonMesh pmesh)
        {
            var weights = new double[pmesh.Halfedges.Count / 2];
            for (int h = 0; h < pmesh.Halfedges.Count; h++)
            {
                // get the opposite halfedge
                var ho = pmesh.Halfedges.GetPairHalfedge(h);

                var a = pmesh.Vertices[pmesh.Halfedges[h].StartVertex].ToVector3D();
                var c = pmesh.Vertices[pmesh.Halfedges[ho].StartVertex].ToVector3D();

                if (pmesh.Halfedges[h].AdjacentFace != -1)
                {
                    var b = pmesh.Vertices[pmesh.Halfedges[pmesh.Halfedges[h].PrevHalfedge].StartVertex]
                        .ToVector3D(); // prev vertex

                    var ba = a - b;
                    var bc = c - b;

                    double cot = Cotan(ba, bc);
                    weights[h >> 1] += Clamp_Cot(cot) * 0.5;
                }

                if (pmesh.Halfedges[ho].AdjacentFace != -1)
                {
                    var d = pmesh.Vertices[pmesh.Halfedges.EndVertex(pmesh.Halfedges[ho].NextHalfedge)]
                        .ToVector3D(); // prev vertex

                    var da = a - d;
                    var dc = c - d;

                    double cot = Cotan(da, dc);
                    weights[h >> 1] += Clamp_Cot(cot) * 0.5;
                }
            }

            return weights;
        }
    }
}
