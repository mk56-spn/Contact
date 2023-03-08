using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Shaders;
using osuTK;

namespace osu.Game.Rulesets.Contact.Objects.Shapes;

public partial class Polygon : Drawable
{
    private IShader shader = null!;

    [BackgroundDependencyLoader]
    private void load(ShaderManager shaderManager)
    {
        shader = shaderManager.Load(VertexShaderDescriptor.TEXTURE_2, "PolygonShader");
    }

    protected override DrawNode CreateDrawNode() => new ShaderCDrawNode(this);

    private class ShaderCDrawNode : DrawNode
    {
        public new Polygon Source => (Polygon)base.Source;

        public ShaderCDrawNode(IDrawable source)
            : base(source)
        {
        }

        private IShader shader;
        private Vector2 drawSize;
        private Vector4 colour;

        public override void ApplyState()
        {
            base.ApplyState();

            shader = Source.shader;
            drawSize = Source.DrawSize;

            colour = new Vector4(0.3f, 0.5f, 0.2f, 1);
        }

        public override void Draw(IRenderer renderer)
        {
            base.Draw(renderer);

            shader.Bind();

            Quad quad = new Quad(
                Vector2Extensions.Transform(Vector2.Zero, DrawInfo.Matrix),
                Vector2Extensions.Transform(new Vector2(drawSize.X, 0f), DrawInfo.Matrix),
                Vector2Extensions.Transform(new Vector2(0f, drawSize.Y), DrawInfo.Matrix),
                Vector2Extensions.Transform(drawSize, DrawInfo.Matrix)
            );

            renderer.DrawQuad(renderer.WhitePixel, quad, DrawColourInfo.Colour);
        }
    }
}
