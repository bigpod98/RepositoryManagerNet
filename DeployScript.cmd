cd KubernetesFiles/RepositoryManagerNet
kubectl apply -f Namespace.yaml 
kubectl apply -f Jobs.yaml
kubectl apply -f Deployment.yaml
kubectl apply -f Service.yaml
kubectl apply -f Ingress.yaml
cd ..
cd ..
