#!/bin/bash

# set environment variables used in deploy.sh and AWS task-definition.json:
export IMAGE_NAME=coreinvestmenttracker
export IMAGE_VERSION=latest

export AWS_DEFAULT_REGION=eu-west-2
export AWS_ECS_CLUSTER_NAME=default
export AWS_VIRTUAL_HOST=loadbalancer-2114224881.eu-west-2.elb.amazonaws.com

# set any sensitive information in travis-ci encrypted project settings:
# required: AWS_ACCOUNT_NUMBER, AWS_ACCESS_KEY_ID, AWS_SECRET_ACCESS_KEY
# optional: SERVICESTACK_LICENSE