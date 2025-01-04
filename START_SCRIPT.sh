#!/bin/bash

# Configuration
DOCKER_COMPOSE_FILE="docker-compose.yaml"

# Helper functions
function start_containers {
    echo "Starting Docker Compose containers..."
    docker-compose -f $DOCKER_COMPOSE_FILE up -d
}

function build_containers {
    echo "Building Docker images..."
    docker-compose -f $DOCKER_COMPOSE_FILE build
}

function stop_containers {
    echo "Stopping Docker Compose containers..."
    docker-compose -f $DOCKER_COMPOSE_FILE down
}

function clean_volumes {
    echo "Stopping and removing containers, networks, and volumes..."
    docker-compose -f $DOCKER_COMPOSE_FILE down --volumes --remove-orphans
}

# Menu loop
while true; do
    echo
    echo "Select an option:"
    echo "1) Start containers (docker-compose up)"
    echo "2) Build images (docker-compose build)"
    echo "3) Stop containers (docker-compose down)"
    echo "4) Stop and remove volumes (down + volumes)"
    echo "5) Rebuild images and start containers (build + up)"
    echo "0) Exit"
    echo

    read -p "Choose an option (0-5): " option

    case $option in
        1)
            start_containers
            ;;
        2)
            build_containers
            ;;
        3)
            stop_containers
            ;;
        4)
            clean_volumes
            ;;
        5)
            build_containers
            start_containers
            ;;
        0)
            echo "Exiting script."
            break
            ;;
        *)
            echo "Invalid option! Please try again."
            ;;
    esac
done
