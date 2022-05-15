using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

public class Game : STGFrame{
    //スコア
    public static int score = 0;
    public static int wave = 1;

    //星に関して
    public static readonly int STAR_MAX = 300;
    public static Star[] stars;

    //スプライト
    public static Bitmap[] sprites;
    public static Player player = new Player();
    public static List<Bullet> Pbullets = new List<Bullet>();

    //敵
    public static Bitmap[] enemys;
    public static List<Enemy> enes = new List<Enemy>();
    public static List<Bullet> Ebullets = new List<Bullet>();

    //玉、エフェクト
    public static Bitmap[] bullets;
    public static Color colors = Color.Black;

    public static Bitmap[] explos;
    public static List<Expl> expls = new List<Expl>();

    //その他
    public static bool changeColor = false;
    public static int changeTime = 2;

    //ゲームに関わる事
    public static int timer = 0;
    public static int bossNum = 1;

    public static void init(){
        player = new Player();
        Pbullets = new List<Bullet>();
        enes = new List<Enemy>();
        Ebullets = new List<Bullet>();

        score = 0;
        wave = 1;
        timer = 0;
        bossNum = 1;

        colors = Color.Black;
        changeColor = false;
        changeTime = 2;
    }

    public static void usuallyIn(bool init){
        if(changeColor && changeTime > 0){
            colors = Color.Red;
            changeTime--;
            if(changeTime <= 0){
                changeColor = false;
            }
        }else{
            changeTime = 2;
            colors = Color.Black;
        }

        SolidBrush brush = new SolidBrush(colors);
        vg.FillRectangle(brush, camera_x - 2, camera_y - 2, SCREEN_W + 4, SCREEN_H + 4);//プラスしてるのは端っこが白くなることを防ぐため。
        colors = Color.Black;
        
        if(init){
            stars = new Star[STAR_MAX];
            for (int i = 0; i < stars.Length; i++){
                stars[i] = new Star(ran.Next(FIELD_W),ran.Next(FIELD_H));
                stars[i].draw();
            }
            MkSomething.mkSprite();//spritesの配列を完成させるやつ
            MkSomething.mkEnemy();
            MkSomething.mkBullet();
            MkSomething.mkExplor();
            
        }else{
            for (int i = 0; i < stars.Length; i++){
                stars[i].update();
                stars[i].draw();
            }
        }
    }
    
    public static void gameLoop(){
        //追加
        if(timer < 40 * 20){
            if(ran.Next(wave <= 5 ? 20 - wave * 2 : 5) == 1)
            enes.Add(new Enemy(0, ran.Next(FIELD_W), 0, 0, 4));
        }else if(timer > 40 * 20){
            if(bossNum == 1){
                enes.Add(new Enemy(4, FIELD_W / 2, 0, 0, 0));
                bossNum -= 1;
            }
            if(enes.Count == 0){
                timer = 0;
                bossNum = 1;
                wave++;
            }
        }
        
        timer++;
        //update
        for(int i = 0; i < enes.Count; i++){
            enes[i].update();
            if(enes[i].kill){
                enes.RemoveAt(i);//ここもちょいと違う。
                i--;
            }
        }
        for(int i = 0; i < Pbullets.Count; i++){//Countで要素数取得。[i]とかで要素を指定できる。javaのArrayListとはちょっと違うからちゅうい
            Pbullets[i].update();
            if(Pbullets[i].kill){
                Pbullets.RemoveAt(i);//ここもちょいと違う。
                i--;
            }
        }
        for(int i = 0; i < Ebullets.Count; i++){//Countで要素数取得。[i]とかで要素を指定できる。javaのArrayListとはちょっと違うからちゅうい
            Ebullets[i].update();
            if(Ebullets[i].kill){
                Ebullets.RemoveAt(i);//ここもちょいと違う。
                i--;
            }
        }
        for(int i = 0; i < expls.Count; i++){
            expls[i].update();
            if(expls[i].kill){
                expls.RemoveAt(i);
                i--;
            }
        }
        player.update();

        //描画
        for(int i = 0; i < expls.Count; i++){
            expls[i].draw();
        }

        for(int i = 0; i < Pbullets.Count; i++){
            Pbullets[i].draw();
        }
        for(int i = 0; i < Ebullets.Count; i++){
            Ebullets[i].draw();
        }
        for(int i = 0; i < enes.Count; i++){
            enes[i].draw();
        }
        if(!player.kill){
            player.draw();
        }
    }

    public static void DrawSomething(Graphics e){
        Font font;
        if(DEBUG){
            //DrawString(どんな文字を, どんな書式で, どんな色で,　どこにX, どこにY);
            font = new Font("Meiryo", 16);
            e.DrawString("FPS:" + FPS, font, Brushes.White, 10, 0);
            e.DrawString("Tama:" + Pbullets.Count, font, Brushes.White, 10, 20);
            e.DrawString("Teki:" + enes.Count, font, Brushes.White, 10, 40);
            e.DrawString("tekiTama:" + Ebullets.Count, font, Brushes.White, 10, 60);
            e.DrawString("HP:" + player.hp, font, Brushes.White, 10, 80);
            e.DrawString("SCRORE:" + score, font, Brushes.White, 10, 100);
            e.DrawString("Timer:" + timer, font, Brushes.White, 10, 120);
            e.DrawString("Wave:" + wave, font, Brushes.White, 10, 140);
        }else if(!DEBUG && !player.kill){
            font = new Font("Meiryo", 20);
            e.DrawString("SCORE:" + score, font, Brushes.White,  10, CANVAS_H - 80);
            e.DrawString("WAVE:" + wave, font, Brushes.White,  10, CANVAS_H - 110);
        }
        int pHpw = CANVAS_W - 20;
        float vpHpw = player.hp / player.hpm * pHpw;

        SolidBrush brush = new SolidBrush(Color.FromArgb(150, 0, 0, 255));
        e.FillRectangle(brush, 10, CANVAS_H - 35, vpHpw, 20);
        Pen pen2 = new Pen(Color.FromArgb(230, 0, 0, 255), 2);
        e.DrawRectangle(pen2, 10, CANVAS_H - 35, pHpw, 20);

        for(int i = 0; i < enes.Count; i++){
            if(enes[i].anime == 4){
                int eHpw = CANVAS_W - 20;
                float veHpw = enes[i].hp / enes[i].hpm * eHpw;

                brush = new SolidBrush(Color.FromArgb(150, 255, 0, 0));
                e.FillRectangle(brush, 10, 10, veHpw, 20);
                pen2 = new Pen(Color.FromArgb(230, 255, 0, 0), 2);
                e.DrawRectangle(pen2, 10, 10, eHpw, 20);
            }
        }

        font = new Font("Meiryo", 40);
        if(player.kill){
            e.DrawString("GAME OVER", font, Brushes.Red, CANVAS_W / 2 - 170, 150);
            
            font = new Font("Meiryo", 20);
            e.DrawString("SCORE:" + score, font, Brushes.Red,  200, 230);
        }
    }
}
