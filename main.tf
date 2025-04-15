provider "azurerm" {
  features {}
}

# Variables (inline for simplicity)
variable "location" {
  default = "East US"
}

# Resource Group
resource "azurerm_resource_group" "rg" {
  name     = "codecraft-rg"
  location = var.location
}

# Azure Container Registry (ACR)
resource "azurerm_container_registry" "acr" {
  name                = "acrsid65764534"
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location
  sku                 = "Basic"
  admin_enabled       = false
}

# Azure Kubernetes Service (AKS)
resource "azurerm_kubernetes_cluster" "aks" {
  name                = "codecraft-aks"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  dns_prefix          = "codecraft"

  default_node_pool {
    name       = "default"
    node_count = 1
    vm_size    = "Standard_DS2_v2"
  }

  identity {
    type = "SystemAssigned"
  }

  role_based_access_control {
    enabled = true
  }

  network_profile {
    network_plugin = "azure"
  }
}

# Allow AKS to pull from ACR
resource "azurerm_role_assignment" "acr_pull" {
  scope                = azurerm_container_registry.acr.id
  role_definition_name = "AcrPull"
  principal_id         = azurerm_kubernetes_cluster.aks.kubelet_identity[0].object_id
}

# Output kubeconfig (for local use or Jenkins setup)
output "kube_config" {
  value     = azurerm_kubernetes_cluster.aks.kube_config_raw
  sensitive = true
}
