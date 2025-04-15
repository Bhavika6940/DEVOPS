pipeline {
  agent any
  environment {
    ACR_NAME = 'acrsid12345'
    IMAGE_NAME = 'webapidocker1'
    RESOURCE_GROUP = 'codecraft-rg'
    CLUSTER_NAME = 'codecraft-aks'
  }

  stages {
    stage('Checkout') {
      steps {
        git url:'https://github.com/Bhavika6940/DEVOPS', branch: 'main'
      }
    }
     stage('Build .NET App') {
            steps {
                bat 'dotnet publish ProductService/ProductService/ProductService.csproj -c Release -o out'
            }
        }

    stage('Build Docker Image') {
      steps {
        sh "docker build -t $ACR_NAME.azurecr.io/$IMAGE_NAME:latest -f ProductService/Dockerfile ProductService"
      }
    }
    
    stage('Push to ACR') {
      steps {
        withCredentials([azureServicePrincipal('AZURE_CREDENTIALS')]) {
          sh '''
            az login --service-principal -u $AZURE_CLIENT_ID -p $AZURE_CLIENT_SECRET --tenant $AZURE_TENANT_ID
            az acr login --name $ACR_NAME
            docker push $ACR_NAME.azurecr.io/$IMAGE_NAME:latest
          '''
        }
      }
    }

    stage('Deploy to AKS') {
      steps {
        withCredentials([azureServicePrincipal('AZURE_CREDENTIALS')]) {
          sh '''
            az aks get-credentials --resource-group $RESOURCE_GROUP --name $CLUSTER_NAME
            kubectl apply -f k8s/test.yaml
          '''
        }
      }
    }
  }
}
