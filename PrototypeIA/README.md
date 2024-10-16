![MCN](https://github.com/CQEN-QDCE/.github/blob/main/images/mcn.png)


# Prototype IA

---

## **Introduction**

Le prototype de l'IA est une composante cruciale de notre projet de vitrine pour le ministère de la cybersécurité et du numérique du Québec (MCN). Il représente une innovation majeure dans notre approche, introduisant un guide IA intelligent qui assiste et oriente les utilisateurs à travers la vitrine.

La décision de développer le prototype de l'IA séparément de Spatial.io n'a pas été prise à la légère. Spatial.io, bien que puissant pour créer des mondes immersifs, présente certaines limitations en ce qui concerne l'intégration de code personnalisé. Ces restrictions nous ont empêchés d'implémenter directement l'IA sophistiquée que nous visions dans le monde de Spatial.io.

## **But du Prototype**

Le but de ce prototype est de démontrer la faisabilité et l'efficacité d'un guide IA dans un environnement de réalité virtuelle (RV). En créant un projet séparé, nous avons pu explorer et développer des fonctionnalités d'IA avancées sans être entravés par les contraintes de Spatial.io. Le résultat est un guide interactif qui peut répondre aux questions, fournir des informations et enrichir l'expérience globale de la vitrine.

---

## Pré-requis

1. Un compte Azure
2. Créer les ressources nécessaires
    - Azure Search : il faut faire un index et le lier avec le blob storage pour qu'il puisse faire une recherche dans les sources
    - Azure Speech Service
    - Storage : il faut créer un blob storage et télécharger les sources. Pour le projet on a utiliser des PDF et séparer chaque page pour un seul sujet.
    - Azure Open AI : il faut créer un déploiement dans Azure OpenAI Studio et choisir le modèle gpt-35-turbo.
3. Dans unity il faut appelé les services azure à l'aide des éléments nécessaire pour que l’IA fonctionne correctement.
    - Azure Search : il faut le nom de la ressource et la clé
    - Azure Speech : il faut la clé et la région, même chose pour la reconnaissance vocal et la synthèse vocal
    - Azure Storage : besoin de rien dans unity
    - Azure Open AI : il faut le endpoint, la clé et le nom du déploiement
    
    ---
    
    ## Fonctionnement d’Azure Search
    
    Afin d'améliorer la recherche de sources par l'IA et lui permettre de fournir des réponses plus précises, il sera nécessaire d'activer et d'ajouter la fonctionnalité "Semantic Search" dans Azure Cognitive Search.
    
    Vous trouverez ci-dessous une vidéo qui explique clairement comment procéder :
    
    [How to make your data searchable with Azure Search and AI | Azure Tips and Tricks](https://www.youtube.com/watch?v=OQDRNQD1LDk)
    
    ## Exemple d’utilisation du IA
    
    L'IA peut être expérimentée sans avoir besoin d'un casque de réalité virtuelle (RV). Il vous suffit de placer "XR Interaction Manager" (votre personnage RV) dans le périmètre de conversation de l'IA. Ensuite, en appuyant sur "Play" en haut de votre écran, l'IA commencera immédiatement à dialoguer avec vous. Vous devrez alors lui poser une question pour continuer l'interaction.


    
    Toutefois, si vous souhaitez tester le périmètre et certaines fonctions de l'IA, il serait préférable de le faire à l'aide de votre casque de réalité virtuelle (RV). Vous trouverez [**ici](https://circuitstream.com/blog/oculus-link)** les instructions sur la manière de connecter votre casque à votre ordinateur. Unity permet de tester le monde tout en ayant le casque RV.
    
    Vous disposerez d'un bouton "Arrêter la conversation" qui vous permettra de terminer la discussion avec l'IA et d'en entamer une nouvelle en posant une question en premier. Si vous sortez du périmètre, la conversation sera réinitialisée, et l'IA ne conservera aucun souvenir de vos échanges précédents (le bouton ne marche pas si le casque n’est pas mis).
