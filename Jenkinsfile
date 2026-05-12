properties([pipelineTriggers([githubPush()])])

pipeline {
  agent {
    node {
      label 'draupnir'
    }
  }

  environment {
    IMAGE_NAME = "books-api"
  }

  stages {
    stage('Build') {
      steps {
        script {
          env.GIT_SHA = sh(script: "git rev-parse --short HEAD", returnStdout: true).trim()

          sh '''
            echo "Building ${IMAGE_NAME}:${GIT_SHA}"

            docker build \
              -t ${IMAGE_NAME}:${GIT_SHA} \
              -t ${IMAGE_NAME}:latest \
              BooksApi
          '''
        }
      }
    }

    stage('Deploy') {
      steps {
        script {
          sh '''
            echo "Deploying ${IMAGE_NAME}:${GIT_SHA}"

            COMPOSE_PROJECT_NAME=${IMAGE_NAME} \
            TAG=${GIT_SHA} \
            docker compose --env-file /opt/books-api/.env up -d
          '''
        }
      }
    }
  }
}