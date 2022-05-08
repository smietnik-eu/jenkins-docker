pipeline {
  environment {
    imagename = "docker-test"
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
        sh "sleep 5"
      }
    }
    stage('Test container, expect http code 200') {
      steps{
        script {
          def response = httpRequest httpMode: 'GET', url: "127.0.0.1:84", validResponseCodes: "200", ignoreSslErrors: true
	  echo "response: ${response.code}"
	  sh "sleep 1"
        }
      }
    }
    stage('Stop and remove docker container') {
      steps{
        sh "docker stop $imagename-$BUILD_NUMBER"
        sh "docker rmi -f  $imagename:$BUILD_NUMBER"
      }
    }

  }
}