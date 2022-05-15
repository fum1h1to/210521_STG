using System;

public class Bullet {
    public float x;
    public float y;
    public float xspeed;
    public float yspeed;
    public float r;
    public float gosaX = 0;
    public float gosaY = 0;
    public int num;
    public bool kill;
    
    public Bullet(float x, float y, float vx, float vy, int num){
        this.x = x;
        this.y = y;
        this.xspeed = vx;
        this.yspeed = vy;
        this.num = num;
        this.kill = false;
        if(num == 0){
            this.r = 3;
            this.gosaX = 0;
            this.gosaY = -5;
        }
        if(num == 2){
            this.r = 4;
        }
    }

    public void update(){
        this.x += xspeed;
        this.y += yspeed;
        
        if(this.x < STGFrame.camera_x || this.x > STGFrame.camera_x + STGFrame.SCREEN_W
        || this.y < STGFrame.camera_y || this.y > STGFrame.camera_y + STGFrame.SCREEN_H ){
            this.kill = true;
        }
    }

    public void draw(){
        MkSomething.DrawBullet(this.num, this.x, this.y);
    }
}