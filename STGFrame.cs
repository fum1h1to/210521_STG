using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

public class STGFrame : Form{
    public static bool DEBUG = false;//デバッグ情報の表示
    //変数定義
    public static readonly int SCREEN_W = 280;//SCREENはFIELDの実際に映るとこ。それを
    public static readonly int SCREEN_H = 320;
    public static readonly int CANVAS_W = SCREEN_W * 2;//実際に映すとこはここ。これによってSCREENを拡大したりすることが後で可能。
    public static readonly int CANVAS_H = SCREEN_H * 2;
    public static readonly int FIELD_W = 400;//仮想画面
    public static readonly int FIELD_H = 400;
    protected static int SCR_WIN = 1;//0がスタート画面、1がゲーム画面、2がゲームオーバーがめん
    public static int FPS = 0;
    public static int drawCount = 0;
    //スクリーンおよび仮想スクリーン作成。のちにCloneで切る抜き
    public static Bitmap screen = new Bitmap(SCREEN_W, SCREEN_H);
    public static Graphics g = Graphics.FromImage(screen);//gはscreenに描画しているという認識でいいのかな？
    public static Bitmap vscreen = new Bitmap(FIELD_W, FIELD_H);
    public static Graphics vg = Graphics.FromImage(vscreen);
    
    //カメラ座標
    public static float camera_x = 0;
    public static float camera_y = 0;

    //いろいろ
    public static Random ran = new Random();

    //key管理
    public static bool KeyA = false;
    public static bool KeyD = false;
    public static bool KeyS = false;
    public static bool KeyW = false;
    public static bool KeySpace = false;
    public static bool KeyEscape = false;
    //public static DateTime old;

    public STGFrame(){
        ClientSize = new Size(CANVAS_W - 1, CANVAS_H - 1);
        Location = new Point(100, 40);
        DoubleBuffered = true;

        //これ入れたら、アンチエイリアスされなくなった！！！！
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        vg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
    }

    protected override void OnLoad(EventArgs e){
        DateTime t = DateTime.Now;
        int old = t.Minute * 60 * 1000 + t.Second * 1000 + t.Millisecond;
        Game.usuallyIn(true);

        Task.Run(() =>{
            while( true ) {
                Game.usuallyIn(false);

                switch(SCR_WIN){
                    case 0:

                    break;
                    case 1:
                        Game.gameLoop();
                        if(KeyEscape){
                            SCR_WIN = 2;
                        }
                    break;
                    case 2:
                        Game.init();
                        SCR_WIN = 1;
                    break;
                }
                draw();

                drawCount++;
                t = DateTime.Now;
                int now = t.Minute * 60 * 1000 + t.Second * 1000 + t.Millisecond;
                int ab = Math.Abs(now - old);
                if(ab >= 1000){
                    FPS = drawCount;
                    drawCount = 0;
                    old = t.Minute * 60 * 1000 + t.Second * 1000 + t.Millisecond;
                }
                Task.Delay(16).Wait();
            }
        });
    }

    protected override void OnKeyDown(KeyEventArgs e){
        if(e.KeyCode == Keys.A )    KeyA      = true;
        if(e.KeyCode == Keys.D )    KeyD      = true;
        if(e.KeyCode == Keys.W )    KeyW      = true;
        if(e.KeyCode == Keys.S )    KeyS      = true;
        if(e.KeyCode == Keys.Space) KeySpace  = true;
        if(e.KeyCode == Keys.Escape)KeyEscape = true;
    }

    protected override void OnKeyUp(KeyEventArgs e){
        if(e.KeyCode == Keys.A )    KeyA      = false;
        if(e.KeyCode == Keys.D )    KeyD      = false;
        if(e.KeyCode == Keys.W )    KeyW      = false;
        if(e.KeyCode == Keys.S )    KeyS      = false;
        if(e.KeyCode == Keys.Space) KeySpace  = false;
        if(e.KeyCode == Keys.Escape)KeyEscape = false;
    }

    protected override void OnPaint(PaintEventArgs e){
        //単位ベクトルのやつと同じ。元のスケールを0～1までの縮尺にして、それを別のスケールで変えてあげる。
        // camera_x = Game.player.x / FIELD_W * (FIELD_W - SCREEN_W);
        // camera_y = Game.player.y / FIELD_H * (FIELD_H - SCREEN_H);
        
        screen = vscreen.Clone(new Rectangle((int)camera_x, (int)camera_y, SCREEN_W, SCREEN_H), vscreen.PixelFormat);
        e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        
        e.Graphics.DrawImage(screen, 0, 0, CANVAS_W, CANVAS_H);//screenをcanvasというサイズで描画。つまりこの場合おおきくする。
        Game.DrawSomething(e.Graphics);

        camera_x = Game.player.x / FIELD_W * (FIELD_W - SCREEN_W);
        camera_y = Game.player.y / FIELD_H * (FIELD_H - SCREEN_H);
    }

    public void draw(){
        Invalidate();
    }
}