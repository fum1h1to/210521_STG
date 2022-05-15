using System;
using System.Drawing;
using System.Windows.Forms;

public class Star{
    public float x;
    public float y;
    public float vx;
    public float vy;
    public float sz;
    
    Random rnd = STGFrame.ran;
    public Star(int x, int y){
        this.x = x;
        this.y = y;
        this.vx = 0;
        this.vy = (float)rnd.NextDouble() + 0.1f;
        this.sz = (float)rnd.NextDouble() + 0.1f;
    }

    public void update(){
        this.x += vx;
        this.y += vy;
        if (this.y > STGFrame.FIELD_H){
            this.y = 0;
            this.x = rnd.Next(STGFrame.FIELD_W);
        }
    }

    public void draw(){
        var caX = STGFrame.camera_x;
        var caY = STGFrame.camera_y;
        if(this.x < caX || this.x > caX + STGFrame.SCREEN_W
        || this.y < caY || this.y > caY + STGFrame.SCREEN_H){
            return;
        }
        Pen pen1 = new Pen(this.sz == 1?Color.FromArgb(200, 200, 200, 230):Color.FromArgb(200,210,210,255), 1);
        STGFrame.vg.DrawRectangle(pen1, this.x, this.y, this.sz, this.sz);
    } 
}