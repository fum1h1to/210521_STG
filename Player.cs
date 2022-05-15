using System;
using System.Drawing;
public class Player{
    public float x;
    public float y;
    public float speed;
    public float r;
    public float hp;
    public float hpm;
    public int muteki;
    public bool mute = false;
    public bool kill;
    public int anime;
    public int reload;
    public int reload2;

    public Player(){
        this.x = STGFrame.FIELD_W / 2;
        this.y = STGFrame.FIELD_H / 2 + 150;
        this.speed  = 3;
        this.anime  = 0;
        this.reload = 10;
        this.reload2 = 0;
        this.r = 7;
        this.hp = 100;
        this.hpm = hp;
        this.kill = false;
        this.muteki = 50;
    }

    public void update(){
        if(!this.kill){
            if(STGFrame.KeySpace && reload == 0){ 
                //Game.Pbullets.Add(new Bullet(this.x, this.y - 20, 0, -5, 1));
                Game.Pbullets.Add(new Bullet(this.x - 3, this.y - 5, -0.2f, -12, 0));
                Game.Pbullets.Add(new Bullet(this.x + 3, this.y - 5,  0.2f, -12, 0));
                Game.Pbullets.Add(new Bullet(this.x - 8, this.y - 5, -0.5f, -12, 0));
                Game.Pbullets.Add(new Bullet(this.x + 8, this.y - 5,  0.5f, -12, 0));
                this.reload = 4;
                this.reload2++;
                if(reload2 == 4){
                    this.reload = 20;
                    reload2 = 0;
                }
            }
            if(this.reload > 0) this.reload--;

            //animeが変わるのもキー入力と同じタイミングじゃん。
        
            if(STGFrame.KeyA && this.x >= this.speed){ 
                this.x -= speed;
                if(this.anime > -8) this.anime--;
            }else{
                if(this.anime < 0) this.anime += 2;
            }
            if(STGFrame.KeyD && this.x <= STGFrame.FIELD_W - this.speed){
                this.x += speed;  
                if(this.anime < 8) this.anime++;
            }else{
                if(this.anime > 0) this.anime -= 2;
            }
            if(STGFrame.KeyW && this.y >= this.speed){
                this.y -= speed;
            }
            if(STGFrame.KeyS && this.y <= STGFrame.FIELD_H - this.speed){
                this.y += speed;
            }
        }

        for(int j = 0; j < Game.Ebullets.Count; j++){
            Bullet test = Game.Ebullets[j];
            
            if(MkSomething.checkHit(this.x, this.y, this.r, test.x, test.y, 4)){
                if(!this.kill && !mute){
                    Game.changeColor = true;
                }
                if(!mute){
                   this.hp -= 10;
                }
                this.mute = true;
                if(this.hp <= 0){
                    this.kill = true;
                    this.hp = 0;
                }else{
                    test.kill = true;
                }
            }  
        }

        for(int j = 0; j < Game.enes.Count; j++){
            Enemy test = Game.enes[j];
            
            if(MkSomething.checkHit(this.x, this.y, this.r, test.x + test.gosaX, test.y + test.gosaY, test.r)){
                if(!this.kill && !mute){
                    Game.changeColor = true;
                }
                if(!mute){
                   this.hp -= 10;
                }
                this.mute = true;
                if(this.hp <= 0){
                    this.kill = true;
                    this.hp = 0;
                }else{
                    if(test.anime != 4)
                    test.kill = true;
                }
            }  
        }

        if(mute){
            muteki--;
        }
        if(muteki <= 0){
            muteki = 50;
            mute = false;
        }
    }

    public void draw(){
        int vanime = this.anime>>2;
        if(!mute){
            MkSomething.DrawSprite(2 + vanime, this.x, this.y);//DrawSpriteメソッドでfieldへの描画もやってくれるからここでは引数を渡すだけでいい。
        }else if(mute && muteki % 10 == 0 || muteki % 10 == 1 || muteki % 10 == 2){
            MkSomething.DrawSprite(2 + vanime, this.x, this.y);
        }
    }
}