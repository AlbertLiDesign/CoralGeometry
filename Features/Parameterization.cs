using Plankton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoralGeometry
{
    public class Parameterization
    {
        public static PlanktonMesh HarmonicMethod(PlanktonMesh pmesh, List<Vector3D> textureVerts)
        {
            List<int> free_vertices = new List<int>();
            int[] idx = new int[pmesh.Vertices.Count];

            int num = 0;
            for (int i = 0; i < pmesh.Vertices.Count; i++)
            {
                if (!pmesh.Vertices.IsBoundary(i))
                {
                    idx[i] = num++;
                    free_vertices.Add(i);
                }
            }
            int n = free_vertices.Count;

            var eweight = LaplaceOperator.CotLaplaceEdgeWeight(pmesh);

            for (int i = 0; i < eweight.Length; i++)
            {
                eweight[i] = Math.Max(0.0, eweight[i]);
            }

            var L = new List<Triplet>();
            var B = new List<Triplet>();

            for (int i = 0; i < n; i++)
            {
                var v = free_vertices[i];
                var hs = pmesh.Vertices.GetHalfedges(v);
                var ww = 0.0;
                Vector3D b = Vector3D.Origin;
                for (int j = 0; j < hs.Length; j++)
                {
                    var vv = pmesh.Halfedges.EndVertex(hs[j]);
                    ww += eweight[hs[j] >> 1];
                    if (pmesh.Vertices.IsBoundary(vv))
                    {
                        b -= -eweight[hs[j] >> 1] * textureVerts[vv];
                    }
                    else
                        L.Add(new Triplet(i, idx[vv], -eweight[hs[j] >> 1]));
                }
                B.Add(new Triplet(i, 0, b.X));
                B.Add(new Triplet(i, 1, b.Y));
                L.Add(new Triplet(i, i, ww));
            }

            double[] X = new double[2 * n];
            Solver.Solve(0, L.ToArray(), L.Count, n, B.ToArray(), B.Count, 2, X);

            for (int i = 0; i < n; i++)
            {
                textureVerts[free_vertices[i]] = new Vector3D(X[i], X[n + i], 0.0);
            }           

            return pmesh;
        }
    }
}
