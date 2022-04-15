using Plankton;
using System.Collections.Generic;
using System.Linq;

namespace CoralGeometry
{
    public static class ExtraPlankton
    {
        public static int[] GetNakedFaces(this PlanktonMesh pmesh)
        {
            List<int> nakedFaces = new List<int>();
            for (int i = 0; i < pmesh.Halfedges.Count; i++)
            {
                if (pmesh.Halfedges[i].AdjacentFace == -1)
                {
                    nakedFaces.Add(pmesh.Halfedges[
                        pmesh.Halfedges.GetPairHalfedge(i)].AdjacentFace);
                }
            }
            return nakedFaces.ToArray();
        }
        public static void UpdateVertex(this PlanktonVertex vert, Vector vec)
        {
            vert.X += (float)vec.X;
            vert.Y += (float)vec.Y;
            vert.Z += (float)vec.Z;
        }
        public static Vector ToVector3D(this PlanktonVertex v)
        {
            return new Vector(v.X, v.Y, v.Z);
        }
        public static Vector ToVertor3D(this PlanktonXYZ xyz)
        {
            return new Vector(xyz.X, xyz.Y,xyz.Z);
        }
    }
}
