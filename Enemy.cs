using System;
using System.Collections.Generic;
public class Enemy{
    public float x;
    public float y;
    public float xspeed;
    public float yspeed;
    public float r;
    public float hp;
    public float hpm;
    public bool mute;
    public float gosaX;
    public float gosaY;
    public int anime;
    public int reload;
    public bool kill;
    public bool flag;
    public int bulPat;

    Random rnd = STGFrame.ran;

    public Enemy(int num, float x, float y, float xspeed, float yspeed){
        this.x = x;
        this.y = y;
        this.xspeed  = xspeed;
        this.yspeed  = yspeed;
        this.anime   = num;
        this.kill = false;
        this.reload  = 10;
        this.flag = false;
        this.bulPat = rnd.Next(2);
        if(num == 0 || num == 1 || num == 2){
            this.r = 11;
            this.gosaX = 0;
            this.gosaY = 3;
            this.mute = false;
            if(Game.wave <= 5){
                this.hp = (Game.wave - 1) * 50 * 2 + 1;
                this.hpm = this.hp;
            }
        }
        if(num == 4){
            this.r = 35;
            this.gosaX = 0;
            this.gosaY = 32;
            this.mute = true;
            if(Game.wave <= 5){
                this.hp = (Game.wave - 1) * 5000 + 10000;
                this.hpm = this.hp;
            }
        }
    }

    public void update(){
        
        if(anime == 0){
            this.mvPart1();
        }else if(anime == 4){
            this.mvBoss();
        }

        this.x += this.xspeed;
        this.y += this.yspeed;
        
        if(this.x < 0 || this.x > STGFrame.FIELD_W
        || this.y < 0 || this.y > STGFrame.FIELD_H ){
            this.kill = true;
        }

        for(int i = 0; i < Game.Pbullets.Count; i++){
            Bullet test = Game.Pbullets[i];
            if(MkSomething.checkHit(this.x + this.gosaX, this.y + this.gosaY, this.r, test.x + test.gosaX, test.y + test.gosaY, test.r)){
                if(!mute){
                    this.hp -= 50;
                }
                if(this.hp <= 0){
                    this.kill = true;
                    Game.expls.Add(new Expl(0, this.x, this.y , 0, 0));
                    if(this.anime == 4){
                        Game.score += (Game.wave - 1) * 2500 + 2500;
                    }else{
                        Game.score += (Game.wave - 1) * 25 + 25;
                    }
                }
                test.kill = true;
                if(this.anime == 4)
                Game.expls.Add(new Expl(0, test.x, test.y , 0, 0));
            }
        }
    }

    public void draw(){
        MkSomething.DrawEnemy(anime, this.x, this.y);
    }

    private void mvPart1(){
        float px = Game.player.x;
        float py = Game.player.y;

        if(this.x > px && this.xspeed > -4 && !this.flag) this.xspeed -= 0.08f;
        else if(this.x < px && this.xspeed < 4 && !this.flag) this.xspeed += 0.08f;

        if(Math.Abs(this.y - py) < 100 && !this.flag){ 
            flag = true;
            switch(bulPat){
                case 0:
                    Game.Ebullets.Add(new Bullet(this.x, this.y , rnd.Next(-2, 2), 5, 2));
                    break;
                case 1:
                    double an;
                    an = Math.Atan2(py - this.y, px - this.x);

                    double vx = Math.Cos(an) * 3;
                    double vy = Math.Sin(an) * 3;
                    Game.Ebullets.Add(new Bullet(this.x, this.y , (float)vx, (float)vy, 2));
                    break;
            }
        }
        if(flag && this.yspeed < 10){
            this.yspeed -= 1.1f;
        }else if(flag && this.x < px && this.xspeed < 4){
            this.xspeed += 1.5f;
        }else if(flag && this.x > px && this.xspeed > -4){
            this.xspeed -= 1.5f;
        }
    }

    private int mvAn = 0;
    private double bulAn = 0;
    private bool fire = false;
    private int fireC = 10;
    private void mvBoss(){

        if(this.y < 140){
            this.yspeed = 0.5f;
            this.mute = true;
        }else{
            this.mute = false;
            mvAn += 1;
            this.yspeed = 0;
            double mvX = Math.Sin(mvAn * Math.PI / 180) * 100;
            this.x = (STGFrame.FIELD_W / 2) + (float)mvX;
            if(mvAn >= 360){
                mvAn = 0;
            }

            if(Game.timer % 20 == 0){
                for(double i = 0; i < Math.PI * 2; i += 0.3){
                    double mvBuX = Math.Cos(i) * 1.5;
                    double mvBuY = Math.Sin(i) * 1.5;
                    Game.Ebullets.Add(new Bullet(this.x, this.y , (float)mvBuX, (float)mvBuY, 3));
                }
            }
            if(Game.timer % 2 == 0){
                double mvBuX = Math.Cos(bulAn) * 1.5;
                double mvBuY = Math.Sin(bulAn) * 1.5;
                Game.Ebullets.Add(new Bullet(this.x, this.y , (float)mvBuX, (float)mvBuY, 4));
                bulAn += 1;
            }

            if(rnd.Next(100) == 0 && this.hp < this.hpm / 2) this.fire = true;

            if(this.fire){
                float px = Game.player.x;
                float py = Game.player.y;
                
                double ang = Math.Atan2(py - this.y, px - this.x);

                double vx = Math.Cos(ang) * 5;
                double vy = Math.Sin(ang) * 5;
                Game.Ebullets.Add(new Bullet(this.x, this.y , (float)vx, (float)vy, 5));

                this.fireC--;
                if(fireC < 0){
                    this.fire = false;
                    this.fireC = 10;
                }
            }
        }
    }
}