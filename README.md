# Giant
------

Giant est un jeu réalisé sous Unity où vous incarnez un géant ayant pour but de se débarrasser de nuisible dans sa paisible forêt.  
Pour cela vous pourrez utilisé vos mains afin de broyer ces nuisibles ou alors des objets qui traînent afin de les écraser.
  
Ce projet utilise le [leap motion controller](https://www.ultraleap.com/product/leap-motion-controller/) afin de détecter les mouvements des mains, c'est un détecteur infrarouge qui traque les mouvements des mains.  
Un peu comme la Kinnect.

# Installation

## Pour jouer

Pour jouer au jeu il vous faudra un [leap motion controller](https://www.ultraleap.com/product/leap-motion-controller/), avoir le logiciel de tracking d'[ultraleap](https://developer.leapmotion.com/tracking-software-download) sur votre ordinateur et le jeu compiler. 

Normallement une version du logiciel et du jeu sont disponibles dans l'onglet release du repo github. 

## Pour le modifier

Pour modifier le projet il vous faudra le logiciel [Unity3D](https://unity.com/fr), ainsi qu'un [leap motion controller](https://www.ultraleap.com/product/leap-motion-controller/), avoir le logiciel de tracking d'[ultraleap](https://developer.leapmotion.com/tracking-software-download) sur votre ordinateur.

De plus il faudra cloner le projet

```
git clone https://github.com/Club-Elec/Giant.git
```

Une fois cela fait je vous recommande de lire la documentation du projet (structure du projet, fonctions, fonctionnement..), ainsi que la documentation du [SDK d'ultraleap pour Unity](https://developer.leapmotion.com/unity).

# Documentation

La structure du projet est expliquée dans [structure.md](Docu/structure.md). 
Les scripts ont des noms assez explicite et sont documentés, mais pour bien comprendre il est recommandé d'avoir des bonnes bases sur la création de jeux avec Unity3D.

# Ressources & Apprendre Unity

Une liste de ressources pour apprendre à utiliser Unity et mieux comprendre ce projet:

- [TutoUnityFR](https://www.youtube.com/@TUTOUNITYFR), un ami qui crée des vidéos pour apprendre à utiliser Unity et faire des projets dessus. Personnellement j'ai appris grâce à ses vidéos. 
- [Le site d'Unity](https://unity.com/fr/learn), permet d'apprendre à utiliser Unity.

Puis des recherches et de la pratique

# Idées à ajoutés

Lors de la présentation du jeu durant la porte ouverte de l'ISEN, plusieurs idées m'ont été donnée pour améliorer le jeu :  
  
- Ajouter un système de timer pour finir la manche
- Afficher les manches restantes
- Ajouter des animations / particules / retour d'interractions

# Bugs connus

Le projet dans l'état actuel comporte plusieurs bugs: 

- Les objets autres que la crate sont parfois bloqués dans le sol et ne peuvent pas être déplacés
- Les mobs ont parfois tendances à marché de travers (une descendance du crabe plus présente chez eux ? )
- Les mobs une fois attrapé reste parfois bloqué dans les airs en ragdoll (problème graphique seulement, ils sont bel et bien détruit une fois le timer finis).

# Contacts

Ce projet à été réalisé par Alban Rouillé,

Si vous avez besoin de me contacter à propos du projet (question, aide, autres..)

Je suis disponible sur discord (Arkagedon#0973), [github](https://github.com/Oxbian) (Oxbian), et matrix (oxbian@matrix.org). 