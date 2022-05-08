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
          dockerImage = docker.build imagename
        }
      }
    }
    stage('run docker image') {
      steps{
        sh "docker run -dit --rm -p 80:80 $imagename:$BUILD_NUMBER"
         sh "curl -o /dev/null -s -w '%{http_code}' localhost:80/index.html && if {{ status == 200 }}; then echo 'all good'; fi"
 
      }
    }
  }
}