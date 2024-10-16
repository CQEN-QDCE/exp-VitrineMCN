![mcnlogoGit](https://github.com/CQEN-QDCE/exp-vitrine-MCN/assets/117953316/792ff943-693a-4404-b101-86186ad80454)



# Vitrine pour Spatial

---

## **Introduction**

La vitrine pour Spatial.io est une composante essentielle du projet du ministère de la cybersécurité et du numérique du Québec (MCN). Elle permet aux utilisateurs d'explorer et de comprendre la mission du MCN à travers une expérience immersive de réalité virtuelle (RV). Cette documentation guide les développeurs et les utilisateurs à travers l'installation, la configuration et l'utilisation de la vitrine sur Spatial.io.

L'un des avantages majeurs de l'utilisation de la plateforme Spatial.io est sa polyvalence et son accessibilité. Grâce à Spatial.io, la vitrine est accessible non seulement aux utilisateurs équipés d'un casque de réalité virtuelle (RV) mais aussi à ceux qui souhaitent explorer le monde via le web. Cette flexibilité élargit considérablement la portée de la vitrine, permettant à un public plus large de s'engager et d'interagir avec le contenu.

---

## Pré-requis

- **Unity** : Version compatible (**Unity 2021.3.21f1**)
- **Spatial.io SDK** : Dernière version (déjà inclus dans le repertoire)

Voici la documentation pour comprendre comment le SDK marche https://docs.spatial.io/getting-started

## Blender

Blender a été un outil essentiel dans le développement de notre projet de vitrine pour le ministère de la cybersécurité et du numérique du Québec (MCN). En tant que logiciel de modélisation 3D open-source et puissant, Blender nous a permis de créer et de personnaliser des éléments pour la vitrine. Par exemple, c’est avec Blender qu’on a créé le batiment.

Cependant, la majorité des objets à l'intérieur de la vitrine ont été créés directement à partir d'Unity.

Pour *télecharger blender voici le lien:*

[https://www.blender.org](https://www.blender.org/)

---

## Exemple d’utilisation avec Blender

### **Exporter un modèle 3D créé sur Blender:**

1. Ouvrir Blender, aller dans la barre du haut et cliquer sur File.
2. Sélectionner Export, puis FBX (.fbx).
3. Choisir l'emplacement que prendra le fichier et cliquer sur Export FBX.

### **Importer un modèle 3D (.fbx) afin de le modifier dans Blender**

1. Ouvrir Blender, aller dans la barre du haut et cliquer sur File.
2. Sélectionner Import, puis FBX (.fbx).
3. Choisir le bon fichier et cliquer sur Import FBX en bas à droite.

### **Modifier la texture d'un objet Blender**

1. Sélectionner l'objet sur lequel vous voulez changer la texture.
2. Cliquer sur Shading dans la barre du haut.
3. Un panneau à droite va s'ouvrir. Sélectionner le menu entouré en bleu (voir ci-dessous) et choisir la texture que vous désirez.

![1](https://github.com/CQEN-QDCE/exp-vitrine-MCN/assets/117953316/4dbd82f1-7fe1-47ef-8dc3-531555f604fe)


### **Modifier le texte des panneaux bleus directionnels**

1. Cliquer sur le texte que vous voulez changer.
2. Appuyer sur TAB, puis saisir le texte désiré.
3. Si jamais le texte devient trop petit ou trop gros, appuyer de nouveau sur TAB, puis, tout en maintenant la touche S enfoncée, faire bouger votre souris vers la gauche (pour rapetisser) ou vers la droite (pour grossir).

Pour plus d’information, voici le guide que Blender offre: https://docs.blender.org/manual/en/latest/interface/window_system/introduction.html

## Exemple d’utilisation avec Unity

### **Importer un modèle FBX (.fbx) dans Unity**

1. Cliquer sur l'onglet Asset dans la barre du haut.
2. Cliquer sur Import New Asset…
3. Sélectionner le bon objet 3D.
- * Si jamais l'objet Blender existe déjà dans Unity et que vous voulez importer une nouvelle version de celui-ci, il faut supprimer cet objet de la section Projet dans Unity avant d'importer la nouvelle version de l'objet.

### **Sélectionner les bonnes textures et les appliquer à l'objet 3D nouvellement importé**

1. Sélectionner l'objet 3D importé; dans Inspector, une fenêtre va s'ouvrir.
2. Sélectionner Materials. En dessous de On Demand Remap, une liste de textures va s'afficher.

![1](https://github.com/CQEN-QDCE/exp-vitrine-MCN/assets/117953316/989ef1c0-3210-42e9-9757-a11cf7a9b4a1)


3. Cliquer sur la cible à côté des matériaux et trouver le matériel correspondant inclus dans le fichier Unity. Seuls Concrete, Dirt, Light et Marble ont besoin d'une texture. Laisser à None pour le reste.

### **Remettre le bâtiment au bon emplacement si jamais il est déplacé par accident**

1. Cliquer sur BuildingAvecDeco dans la section Hierarchy.
2. Copier dans le presse-papier ceci : Vector3(15.8199997,-9.53674316e-07,12.9499998).
3. Faire un clic droit sur Transform et sélectionner Paste, puis Position.
4. La rotation reste toujours (0,0,0) et le scale (1,1,1).

### **Placer des plantes ou des arbres dans la scène Unity**

1. Faire un clic gauche sur le terrain, une fenêtre va apparaître dans l'Inspector.
2. Dans la section Terrain, sélectionner Paint Trees.
    
    ![1](https://github.com/CQEN-QDCE/exp-vitrine-MCN/assets/117953316/c3daacdb-d4d7-4b20-ae37-819679e8e86f)

    
3. Si jamais l'arbre ou la plante ne se trouve pas dans la liste, sélectionner Edit Trees, puis Add Tree, ensuite Tree Prefab, et choisir le bon objet.
4. Sélectionner le bon élément dans la section Trees et ajuster les bons paramètres, tels que Brush Size et Tree Density, qui permettent d'améliorer la précision du placement des éléments et de contrôler le nombre d'éléments placés, respectivement.
5. Placer les objets aux endroits désirés. 

---

### Se connecter avec Spatial SDK

Dans Unity, vous devez vous connecter avec un compte Spatial afin de pouvoir effectuer des tests et publier la scène principale sur la plateforme Spatial.io. Voici les étapes à suivre:

1. Cliquer sur les parametre en haut à droite

![1](https://github.com/CQEN-QDCE/exp-vitrine-MCN/assets/117953316/d1d49f50-e71b-4b24-8d87-58bb5e275891)


2. Ensuite, dans la section "Account", suivez les étapes qui vous sont demandées. Vous devrez simplement copier un token et le coller dans Unity, ce qui vous connectera à votre compte.

![2](https://github.com/CQEN-QDCE/exp-vitrine-MCN/assets/117953316/b3ff7d00-7787-4b63-9e32-15de7cba7857)


### Faire des test avec le monde RV

Une fois que vous avez connecté votre compte, le Spatial SDK vous permet d'effectuer des tests (simulations) de votre monde afin de vous assurer que tout est à votre satisfaction avant de le publier. C'est simple : il vous suffit de cliquer sur le bouton "Test Active Scene", et vous serez redirigé vers une page web où vous vous trouverez avec votre personnage dans votre monde.

![3](https://github.com/CQEN-QDCE/exp-vitrine-MCN/assets/117953316/0a866089-7a29-4e75-9ff7-860860cd723a)


Il est important de noter que toute modification apportée dans votre "Test Active Scene" ne sera pas prise en compte une fois que vous aurez publié votre monde. Par conséquent, vous devrez réappliquer ces modifications après avoir publié votre monde.

### Publier le monde dans Spatial

Pour publier le monde sur Spatial.io, la procédure est simple. Il suffit de se rendre dans les paramètres, puis dans la section "Config". Là, vous pourrez sélectionner la scène souhaitée, nommer votre monde et choisir l'image de couverture qui sera visible par les autres utilisateurs de Spatial.

![4](https://github.com/CQEN-QDCE/exp-vitrine-MCN/assets/117953316/d2bd3a68-7058-43d6-a6cc-2b48478dfd1e)


### Partager les panneaux explicatifs sur Spatial

En ce qui concerne les panneaux, nous avons fait appel à [Canva](https://www.canva.com), un site web spécialisé dans la création de panneaux explicatifs. Il est crucial que les dimensions de ces panneaux soient de 1728 x 2304 px. Voici comment procéder :

1. Ouvrez Canva et cliquez sur le bouton "Créer" situé en haut à droite.

![5](https://github.com/CQEN-QDCE/exp-vitrine-MCN/assets/117953316/b925cc43-5677-4ad1-a23f-fd7b9ba43fd6)


2. Sélectionnez "Taille personnalisée" et entrez les dimensions mentionnées précédemment.
3. Ensuite, vous pourrez ajouter du texte et personnaliser des panneaux selon vos préférences.

Après avoir créé des panneaux, pour les intégrer dans le monde de réalité virtuelle (RV), vous devez procéder directement dans Spatial.io.

4. Ouvrez le monde souhaité depuis Spatial.io en accédant à la section "Your spaces", située en haut à droite de l'écran.

![1](https://github.com/CQEN-QDCE/exp-vitrine-MCN/assets/117953316/d0419bbc-6606-4e7e-9949-6eca1e1428fd)


5. Une fois que vous êtes dans le monde publié, vous devez cliquer sur le symbole " + " situé en bas de l'écran.

![1](https://github.com/CQEN-QDCE/exp-vitrine-MCN/assets/117953316/e9820774-32eb-4c22-b88e-5de3cad9b0a4)


6. Ensuite, pour ajouter les panneaux créés ou d'autres contenus, vous devez sélectionner l'option "Upload" et rechercher le contenu souhaité.

![1](https://github.com/CQEN-QDCE/exp-vitrine-MCN/assets/117953316/4beb41c6-1b8f-4f7d-84e0-75d1b4e7be50)

Par ailleurs, nous avons confié les panneaux à Bruno Gagné, qui sera responsable de ceux-ci pour le moment. Il pourra vous inviter dans le groupe Canva afin que vous puissiez apporter des modifications. Ainsi, l'ensemble du groupe pourra voir les changements, ce qui favorisera la collaboration et le travail d'équipe.

### Publier le même monde avec un autre compte

Si vous souhaitez publier un monde Spatial avec un compte différent, vous devrez suivre les étapes ci-dessous. Cette procédure est utile si vous travaillez avec plusieurs comptes ou si vous devez transférer la propriété d'un monde à un autre compte.

**Étape 1: Ouvrez Votre Projet dans Unity**

- Ouvrez le projet Unity contenant le monde Spatial que vous souhaitez publier.

**Étape 2: Accédez aux Paramètres du Spatial SDK**

- Dans Unity, allez aux paramètres du Spatial SDK en haut à droite

![1](https://github.com/CQEN-QDCE/exp-vitrine-MCN/assets/117953316/3a106281-6aae-4139-915e-e112284b3840)


**Étape 3: Identifiez-vous avec le Premier Compte**

- Connectez-vous avec le compte actuellement associé au monde Spatial.

**Étape 4: Supprimez le Package**

- Sélectionnez "Supprimer" et choisissez la scène appropriée pour retirer le package des paramètres.

![1](https://github.com/CQEN-QDCE/exp-vitrine-MCN/assets/117953316/ae0f570a-478e-47bc-880c-b24012a2561a)


**Étape 5: Déconnectez-vous du Premier Compte**

- Déconnectez-vous du compte actuellement associé au monde Spatial.

**Étape 6: Connectez-vous avec le Compte Différent**

- Connectez-vous avec le compte avec lequel vous souhaitez publier le monde Spatial.

**Étape 7: Reconfigurez et Publiez**

- Reconfigurez les paramètres nécessaires pour le monde Spatial avec le nouveau compte.
- Cliquez sur "Publier" pour publier le monde Spatial avec le compte différent.

**Étape 8: Vérifiez la Publication**

- Connectez-vous à Spatial.io avec le nouveau compte et vérifiez que le monde a été publié correctement.

---

## Limites à respecter avec Spatial SDK

Le projet à été créer avec la version gratuit de spatial, donc il y a certaines normes qu’il faut respecter pour que le monde sois publier et marche bien.

Vous serez confrontés à une limite de construction dans Unity que vous devrez respecter. Cette limite est fixée à 500k Mesh Vertices. Pour ce projet, nous avons atteint 492k, donc il est important de faire preuve de prudence. Je vous encourage à prendre en compte les normes situées en bas à droite de la scène.
![1](https://github.com/CQEN-QDCE/exp-vitrine-MCN/assets/117953316/5222a333-86c0-4f30-a3ad-f6698dfba2f7)

