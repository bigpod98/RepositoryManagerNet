#!/bin/sh
mkdir /repository/conf
touch /repository/conf/distributions


echo "Origin: emptytest" >> /repository/conf/distributions
echo "Label: emptytest" >> /repository/conf/distributions
echo "Codename: main" >> /repository/conf/distributions
echo "Architectures: amd64" >> /repository/conf/distributions
echo "Components: main" >> /repository/conf/distributions
echo "Description: examle repo" >> /repository/conf/distributions