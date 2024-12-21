
// This class is credited to: @Tjstretchalot on Github
class IntersectionChecker {
    
    public static bool HasIntersectingLines(float[] vertices) {
        int vertexCount = vertices.Length / 2;
        for (int i = 0; i < vertexCount; i++) {
            float ax1 = vertices[2 * i];
            float ay1 = vertices[2 * i + 1];
            float ax2 = vertices[2 * ((i + 1) % vertexCount)];
            float ay2 = vertices[2 * ((i + 1) % vertexCount) + 1];

            for (int j = i + 1; j < vertexCount; j++) {
                float bx1 = vertices[2 * j];
                float by1 = vertices[2 * j + 1];
                float bx2 = vertices[2 * ((j + 1) % vertexCount)];
                float by2 = vertices[2 * ((j + 1) % vertexCount) + 1];

                if (i == j - 1 || (i == 0 && j == vertexCount - 1))
                    continue;

                if (DoLinesIntersect(ax1, ay1, ax2, ay2, bx1, by1, bx2, by2))
                    return true;
            }
        }

        return false;
    }

    private static bool DoLinesIntersect(float x1, float y1, float x2, float y2, float x3, float y3, float x4,
        float y4) {
        float denominator = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
        if (denominator == 0)
            return false;

        float t = ((x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4)) / denominator;
        float u = -((x1 - x2) * (y1 - y3) - (y1 - y2) * (x1 - x3)) / denominator;

        return t > 0 && t < 1 && u > 0 && u < 1;
    }
}