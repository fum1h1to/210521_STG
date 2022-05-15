using System;

public class Expl{
    public float x;
    public float y;
    public float xspeed;
    public float yspeed;
    public int anime;
    public int counter = 0;
    public bool kill;

    public Expl(int num, float x, float y, float vx, float vy){
        this.x = x;
        this.y = y;
        this.xspeed = vx;
        this.yspeed = vy;
        this.anime = num;
    }

    public void update(){
        this.x += this.xspeed;
        this.y += this.yspeed;
        if(this.x < 0 || this.x > STGFrame.FIELD_W
        || this.y < 0 || this.y > STGFrame.FIELD_H ){
            this.kill = true;
        }

        this.anime = counter>>1;
        counter++;
        if(anime >= 10){
            this.kill = true;
        }
    }

    public void draw(){
        MkSomething.DrawExplor(this.anime, this.x, this.y);
    }
}