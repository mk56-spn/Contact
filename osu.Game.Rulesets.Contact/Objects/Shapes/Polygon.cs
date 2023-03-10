using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace osu.Game.Rulesets.Contact.Objects.Shapes;

public partial class Polygon : Sprite
{
    private IShader shader = null!;

    [BackgroundDependencyLoader]
    private void load(ShaderManager shaderManager)
    {
        shader = shaderManager.Load(VertexShaderDescriptor.TEXTURE_2, "MovingPolygon");
    }

    private float iTime;

    protected override void Update()
    {
        base.Update();
        iTime += (float)(Clock.ElapsedFrameTime / 1000f);
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

        public override void ApplyState()
        {
            base.ApplyState();

            shader = Source.shader;
            drawSize = Source.DrawSize;
        }

        public override void Draw(IRenderer renderer)
        {
            base.Draw(renderer);

            shader.Bind();

            shader.GetUniform<float>("iTime").UpdateValue(ref Source.iTime);

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
