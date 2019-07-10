# SVGAPlayer-Unity
The SVGAPlayer implementation of Unity using Shader.

# Introduction

SVGAPlayer is a light-weight animation renderer. This is Unity implementation for it.You can play `.svga` file on all platform.

[Details](http://svga.io/)

# Quickstart

1. Set
 - Canvas Scaler UI Scale Mode is Constant Physical Size 
 
 or 
 - Scale With Screen Size Reference Resolution y = Camera.Size * 200 and Match = 1
2. API

```CS

public void LoadSvgaFileData(Stream svgaFileBuffer){}

public void Play(int times, Action callback = null){}

public void Pause()

```