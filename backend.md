JIT hat den vorteil, dass es über eine längere Laufzeit besser optimiert wird als AOT

AOT hat den vorteil, dass wirklich nur das kompiliert wird, was auch verwendet wird.
dadurch können AOT schnellere Startup times erreichen.

Bei Serverless applikationen oder generell applikationen welche sich schnell starten sollen
ist AOT bevorzugt, da eine schnelle startup time wichtiger ist als ein 100% optimiertes laufen der applikation
vor allem, da die applikation meistens nur kurz verwendet wird.

bsp. Ich verwende pro tag öfters notepad++ das sollt e mit AOT kompiliert werden da ich gerne ein schnelles
startup des programms habe, anstatt dass es 10ms schneller eine aktion durchführen kann.

Bei Server applikationen die immer durchlaufen sollen, würde ich eher JIT verwenden, da die applikation
sich über die Zeit optimiert und noch effizienter läuft