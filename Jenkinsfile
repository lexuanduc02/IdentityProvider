def image = "test_image"
def containerName = "test_container"

pipeline {
    agent any

    stages {
        stage('Delete Docker Container if exists') {
            steps {
                script {
                    try {
                        sh "docker container stop $containerName"
                        sh "docker container rm $containerName"
                        echo "Delete $containerName Done"
                    } catch (Exception e) {
                        echo " $containerName not exists or not running"
                    }
                }
            }
        }

        stage('Delete Docker image if exists') {
            steps {
                script {
                    try {
                        echo "Remove Image"
                        sh "docker image rm $image"
                        echo "Remove Image Done"
                    } catch (Exception e) {
                        echo " $image not exists or not running"
                    }
                }
            }
        }

        stage('Build') {
            steps {
                echo "Check SCM"
                checkout scm
                echo "Check SCM Done"
                echo "Build Image start"
                script {
                    docker.build(image + ":$BUILD_NUMBER", "-f IdentityProvider/Dockerfile .")
                }
                echo "Build Image Done"
            }
        }

        stage('Run') {
            steps {
                echo "Start Build Container"
                sh "docker run -d -p 5040:8080  -e TZ=Asia/Ho_Chi_Minh --restart=always --name=${containerName} ${image}:${BUILD_NUMBER}"
                echo "Build done !"
            }
        }

        stage('Write for Build') {
            steps {
                script {
                    echo "Build Number: ${BUILD_NUMBER}"
                }
            }
        }
    }
}
