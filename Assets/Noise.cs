using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public static class Noise 
{
  static FastNoiseLite maxYNoiseGenerator;
  static FastNoiseLite caveNoiseGenerator;
  static FastNoiseLite maxYMountainOverlayGenerator;
  static FastNoiseLite maxYRoughnessGenerator;
  static FastNoiseLite maxYcaveInGenerator;
  static FastNoiseLite SpaphGenerator;
 static Noise(){
  maxYRoughnessGenerator = new FastNoiseLite(Terrain._SEED);
maxYRoughnessGenerator.SetFractalType(FastNoiseLite.FractalType.FBm);
  SpaphGenerator = new FastNoiseLite(Terrain._SEED);
  maxYcaveInGenerator = new FastNoiseLite(Terrain._SEED);
  maxYcaveInGenerator.SetFrequency(0.01f);
maxYcaveInGenerator.SetFractalLacunarity(2f);
maxYcaveInGenerator.SetFractalGain(0.5f);

SpaphGenerator.SetFrequency(-0.005f);
SpaphGenerator.SetFractalGain(1.1f);
SpaphGenerator.SetFractalWeightedStrength(3.31f);
SpaphGenerator.SetFractalType(FastNoiseLite.FractalType.PingPong);
SpaphGenerator.SetFractalPingPongStrength(0.69f);

  maxYNoiseGenerator = new FastNoiseLite(Terrain._SEED);
  maxYNoiseGenerator.SetFrequency(0.01f);
maxYNoiseGenerator.SetFractalLacunarity(2f);
maxYNoiseGenerator.SetFractalGain(0.5f);  caveNoiseGenerator = new FastNoiseLite(Terrain._SEED);
  caveNoiseGenerator.SetFrequency(0.030f);
caveNoiseGenerator.SetFractalLacunarity(1.07f);
caveNoiseGenerator.SetFractalGain(-4.46f);
caveNoiseGenerator.SetFractalWeightedStrength(-1.09f);
caveNoiseGenerator.SetFractalType(FastNoiseLite.FractalType.FBm);

  maxYMountainOverlayGenerator = new FastNoiseLite(Terrain._SEED +1);
  maxYMountainOverlayGenerator.SetFrequency(0.01f);
maxYMountainOverlayGenerator.SetFractalLacunarity(2f);
maxYMountainOverlayGenerator.SetFractalGain(0.5f);
 }
      public static int GetMaxTerrainGenerationHeightForXCoordinate(int x){
        float noise =  maxYNoiseGenerator.GetNoise(x /0.0001f / Terrain._TERRAIN_SPREAD,x /0.0001f/ Terrain._TERRAIN_SPREAD);
        float roughness = maxYRoughnessGenerator.GetNoise(x*5,x*5);
        float caveIn = maxYcaveInGenerator.GetNoise(x/0.00000001f,x/0.00000001f);
        float overlay = maxYMountainOverlayGenerator.GetNoise(x/0.0001f /10000,x/0.0001f /10000);
        
        return Mathf.RoundToInt(  Terrain._MAX_NEGATIVE_HEIGHT - (math.clamp(noise * math.clamp( overlay,0.0f,1.0f),  -1.0f,  Terrain._TERRAIN_ROUGHNESS) + roughness * 0.05f + math.clamp(caveIn,  0,  Terrain._TERRAIN_ROUGHNESS)* 0.3f)  * 30    ); 
    }
public static bool isSaphire(Vector2 pos){
 if( SpaphGenerator.GetNoise(pos.x,pos.y) > 0.6)
 return true;
 return false; 
}
    public static bool isCave(Vector2 pos){
      
      if(maxYNoiseGenerator.GetNoise(pos.x /5f ,pos.y/ 5f ) * (maxYNoiseGenerator.GetNoise(pos.x /0.05f ,pos.y/ 0.05f ) +maxYNoiseGenerator.GetNoise(pos.x /0.05f ,(pos.y -10)/ 0.05f  ) +maxYNoiseGenerator.GetNoise((pos.x +10) /0.05f ,(pos.y )/ 0.05f  )) > (math.clamp( pos.y *UnityEngine.Random.Range(4.5f,5) * maxYNoiseGenerator.GetNoise(pos.x /0.05f ,pos.y +pos.x/ 0.05f ),-100,100) ) / Terrain._MAX_NEGATIVE_HEIGHT *3 +2.75f )
      return true;
      if(maxYNoiseGenerator.GetNoise(pos.x /0.05f ,pos.y/ 0.05f ) < 0  && (math.abs( pos.y) < 30 && math.abs (pos.y) > 10 || (math.abs (pos.y) > 5  &&math.abs( pos.y) < 35 && UnityEngine.Random.Range(1,10) != 2)   ) )
      return true;

      return false;

    }
}
