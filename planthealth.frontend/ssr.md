build time rendering profitiert davon, dass er die seiten schon vorlädt mit daten und diese Seiten dann statisch abspeichert.
wenn der user die Seite aufruft, gibt er ihn die statische Seite zurück und spart sich damit zbsp die Datenabfragen und verringert 
dadurch die Ladezeiten.

Das ist für unser beispiel nicht so ideal, weil wir von unserer api im sekunden takt neue Daten bekommen, d.h. die Seite die zur 
build time erstellt wurde ist bis dorthin schon längst outdated und spiegelt nicht mehr die aktuellen daten dar.

build Time rendering ist also für dynamische daten die sich sehr rasch ändern nicht so geeignet.