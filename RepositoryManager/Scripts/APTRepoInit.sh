touch /repository/conf/distributions


echo "Origin: emptytest" >> /repository/conf/distributions
echo "Label: emptytest" >> /repository/conf/distributions
echo "Codename: $CODENAME" >> /repository/conf/distributions
echo "Architectures: amd64" >> /repository/conf/distributions
echo "Components: main" >> /repository/conf/distributions
echo "Description: examle repo" >> /repository/conf/distributions