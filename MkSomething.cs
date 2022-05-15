using System;
using System.Drawing;
using System.Windows.Forms;

public class MkSomething : Game{

    //スプライトに関して
    public static void DrawSprite(int num, float vx, float vy){
        float caX = STGFrame.camera_x;
        float caY = STGFrame.camera_y;
        float nw =  sprites[num].Width - 6;
        float nh =  sprites[num].Height - 6;

        var nx = vx - nw / 2;
        var ny = vy - nh / 2;

        if(nx + nw / 2 < caX || nx - nw / 2 > caX + STGFrame.SCREEN_W
        || ny + nh / 2 < caY || ny - nh / 2 > caY + STGFrame.SCREEN_H){
            return;
        }
        vg.DrawImage(sprites[num], nx, ny, nw, nh);
    }
    
    public static void mkSprite(){
        int nspri = 5;
        sprites = new Bitmap[nspri];
        Bitmap sp = new Bitmap("sprite.png");
        
        sprites[0] = sp.Clone(mkMask(0,   0, 22, 42), sp.PixelFormat);
        sprites[1] = sp.Clone(mkMask(23,  0, 33, 42), sp.PixelFormat);
        sprites[2] = sp.Clone(mkMask(57,  0, 43, 42), sp.PixelFormat);
        sprites[3] = sp.Clone(mkMask(101, 0, 33, 42), sp.PixelFormat);
        sprites[4] = sp.Clone(mkMask(135, 0, 21, 42), sp.PixelFormat);

        for(int i = 0; i < nspri; i++){
            sprites[i].MakeTransparent();
        }
    }

    //敵
    public static void DrawEnemy(int num, float x, float y){
        float caX = STGFrame.camera_x;
        float caY = STGFrame.camera_y;
        float nw =  enemys[num].Width;
        float nh =  enemys[num].Height;

        var nx = x - nw / 2;
        var ny = y - nh / 2;

        if(nx + nw < caX || nx - nw > caX + STGFrame.SCREEN_W
        || ny + nh < caY || ny - nh > caY + STGFrame.SCREEN_H){
            return;
        }
        vg.DrawImage(enemys[num], nx, ny, nw, nh);
    }
    
    public static void mkEnemy(){
        int nEnemy = 5;
        enemys = new Bitmap[nEnemy];
        Bitmap spt = new Bitmap("enemy.png");
        
        enemys[1] = spt.Clone(mkMask(  0,  1,  23,  31), spt.PixelFormat);
        enemys[0] = spt.Clone(mkMask( 23,  0,  23,  31), spt.PixelFormat);
        enemys[2] = spt.Clone(mkMask( 48,  1,  23,  31), spt.PixelFormat);
        enemys[3] = spt.Clone(mkMask(  5, 68,  35,  45), spt.PixelFormat);
        enemys[4] = spt.Clone(mkMask(107,  1,  70, 142), spt.PixelFormat);
        
        for(int i = 0; i < nEnemy; i++){
            enemys[i].MakeTransparent();
        }
    }

    //たまとかエフェクト
    public static void DrawBullet(int num, float x, float y){
        float caX = STGFrame.camera_x;
        float caY = STGFrame.camera_y;
        float nw =  bullets[num].Width;
        float nh =  bullets[num].Height;

        var nx = x - nw / 2;
        var ny = y - nh / 2;

        if(nx + nw < caX || nx - nw > caX + STGFrame.SCREEN_W
        || ny + nh < caY || ny - nh > caY + STGFrame.SCREEN_H){
            return;
        }
        vg.DrawImage(bullets[num], nx, ny, nw, nh);
    }
   
    public static void mkBullet(){
        int nBullet = 6;
        bullets = new Bitmap[nBullet];
        Bitmap spb = new Bitmap("bullet.png");
        
        bullets[0] = spb.Clone(mkMask( 2,  2,  6, 16), spb.PixelFormat);
        bullets[1] = spb.Clone(mkMask(41, 12, 50, 50), spb.PixelFormat);

        bullets[2] = spb.Clone(mkMask(11,  2, 8, 8), spb.PixelFormat);
        bullets[3] = spb.Clone(mkMask(11, 10, 8, 8), spb.PixelFormat);
        bullets[4] = spb.Clone(mkMask(11, 18, 8, 8), spb.PixelFormat);
        bullets[5] = spb.Clone(mkMask(11, 26, 8, 8), spb.PixelFormat);
        
        
        for(int i = 0; i < nBullet; i++){
            bullets[i].MakeTransparent();
        }
    }

    //爆発えふぇくと
    public static void DrawExplor(int num, float x, float y){
        float caX = STGFrame.camera_x;
        float caY = STGFrame.camera_y;
        float nw =  explos[num].Width;
        float nh =  explos[num].Height;

        var nx = x - nw / 2;
        var ny = y - nh / 2;

        if(nx + nw < caX || nx - nw > caX + STGFrame.SCREEN_W
        || ny + nh < caY || ny - nh > caY + STGFrame.SCREEN_H){
            return;
        }
        vg.DrawImage(explos[num], nx, ny, nw, nh);
    }
    public static void mkExplor(){
        int nExplo = 10;
        explos = new Bitmap[nExplo];
        Bitmap ex = new Bitmap("explor.png");
        
        explos[0] = ex.Clone(mkMask(   4,  8, 20, 20), ex.PixelFormat);
        explos[1] = ex.Clone(mkMask(  29,  5, 29, 27), ex.PixelFormat);
        explos[2] = ex.Clone(mkMask(  63,  5, 33, 30), ex.PixelFormat);
        explos[3] = ex.Clone(mkMask( 100,  2, 36, 33), ex.PixelFormat);
        explos[4] = ex.Clone(mkMask( 136,  2, 35, 33), ex.PixelFormat);
        explos[5] = ex.Clone(mkMask( 172,  2, 27, 33), ex.PixelFormat);
        explos[6] = ex.Clone(mkMask( 200,  4, 19, 27), ex.PixelFormat);
        explos[7] = ex.Clone(mkMask( 224, 12, 15, 14), ex.PixelFormat);
        explos[8] = ex.Clone(mkMask( 241, 12, 15, 14), ex.PixelFormat);
        explos[9] = ex.Clone(mkMask( 259, 12, 13, 14), ex.PixelFormat);
        
        for(int i = 0; i < nExplo; i++){
            explos[i].MakeTransparent();
        }
    }
    
    //当たり判定
    public static bool checkHit(float x1, float y1, float r1,
                                float x2, float y2, float r2){
        bool back = false;
        float vx = Math.Abs(x1 - x2);
        float vy = Math.Abs(y1 - y2);
        float vr = r1 + r2;
        if((vx * vx + vy * vy) < (vr * vr)){
            back = true;
        }

        return back;
    }

    //補助関数  
    private static Rectangle mkMask(int x, int y, int w, int h){
        Rectangle r = new Rectangle(x, y, w, h);
        return r;
    }

    
}