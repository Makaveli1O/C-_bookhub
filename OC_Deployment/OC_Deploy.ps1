# Powershell script for deploying the BookHuB Web Application into openshift dev sandbox

# consider using also new alias for openshift or replace the "oc" commands
# New-Alias -Name oc -Value C:\Path\to\openshift\oc.exe

# Assumptions (before the script execution):
# 1) Installed Openshift CLI, also registration and own sandbox is required
# 2) oc login (CLI login and connection with your sandbox)
# 3) Docker installed
# 4) Logged in docker hub (choosen as registry service)


# Step 0 -- Set-up the variables (consider changing) 
$script_directory = Get-Location

$docker_build_directory = "..\PV179_BookHub"
$docker_file_name = $args[0].ToString().ToUpper() + "Dockerfile"
$docker_image_name = "540468/pv179:" + $args[0].ToString().ToLower() # consider changing based on your docker hub

$oc_yaml_folder = ".\" + $args[0].ToString().ToUpper() + "\"
$oc_pvc_yaml = $oc_yaml_folder + "pvc.yaml"
$oc_pod_yaml = $oc_yaml_folder + "pod.yaml"


# Step 1 -- Build the image localy and push it to docker hub
cd $docker_build_directory
docker build --no-cache --file $docker_file_name -t $docker_image_name .
docker push $docker_image_name
cd $script_directory


# Step 2 -- Prepare PersistentVolumeClaims in openshift based on yaml file
oc create -f $oc_pvc_yaml


# Step 3 -- Create pod based on yaml file
# NOTE: check the image name and also the PersistentVolumeClaim name if they match
$result = oc create -f $oc_pod_yaml
$pod_name = $result.Split(' ')[0]
$service = $pod_name.Split('/')[1]


# Step 4 -- Expose the pod
oc expose $pod_name


# Step 5 -- Expose the service
oc expose svc $service
