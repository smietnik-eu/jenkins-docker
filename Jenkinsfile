pipeline {
  environment {
    imagename = "docker-test"
    registryCredential = 'kevalnagda'
    dockerImage = ''
  }
  agent any
  stages {
    stage('Cloning Git') {
      steps {
        git([url: 'https://github.com/smietnik-eu/jenkins-docker.git', branch: 'main'])
      }
    }
    stage('Building image') {
      steps{
        script {
          dockerImage = docker.build "${imagename}:${BUILD_NUMBER}"
        }
      }
    }
    stage('run docker image') {
      steps{
        sh "docker run -dit --name $imagename-$BUILD_NUMBER --rm -p 84:80 $imagename:$BUILD_NUMBER"
      }
    }
    stage('Test working container') {
      steps{
         sh "curl -o /dev/null -s -w '%{http_code}' localhost:84/index.html && if {{ status == 200 }}; then echo 'all good'; fi"
      }
    }
    stage('Stop and remove docker container') {
      steps{
        sh "docker stop $imagename-$BUILD_NUMBER"
      }
    }
  }
}