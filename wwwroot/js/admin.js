// Admin Panel JavaScript

document.addEventListener('DOMContentLoaded', function () {
    // Sidebar toggle functionality
    initializeSidebar();

    // Auto-hide alerts
    initializeAlerts();

    // Form enhancements
    initializeForms();

    // Table enhancements
    initializeTables();

    // Initialize tooltips
    initializeTooltips();
});

// Sidebar functionality
function initializeSidebar() {
    const sidebarCollapse = document.getElementById('sidebarCollapse');
    const sidebar = document.getElementById('sidebar');
    const content = document.getElementById('content');

    if (sidebarCollapse) {
        sidebarCollapse.addEventListener('click', function () {
            sidebar.classList.toggle('active');
            content.classList.toggle('active');

            // Store sidebar state in localStorage
            const isActive = sidebar.classList.contains('active');
            localStorage.setItem('sidebarCollapsed', isActive);
        });
    }

    // Restore sidebar state from localStorage
    const sidebarCollapsed = localStorage.getItem('sidebarCollapsed');
    if (sidebarCollapsed === 'true') {
        sidebar.classList.add('active');
        content.classList.add('active');
    }

    // Handle dropdown menus in sidebar
    const dropdownToggles = document.querySelectorAll('.sidebar .dropdown-toggle');
    dropdownToggles.forEach(toggle => {
        toggle.addEventListener('click', function (e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                target.classList.toggle('show');
                this.setAttribute('aria-expanded', target.classList.contains('show'));
            }
        });
    });
}

// Alert functionality
function initializeAlerts() {
    // Auto-hide success alerts after 5 seconds
    const alerts = document.querySelectorAll('.alert-success');
    alerts.forEach(alert => {
        setTimeout(() => {
            if (alert.classList.contains('show')) {
                const bsAlert = new bootstrap.Alert(alert);
                bsAlert.close();
            }
        }, 5000);
    });
}

// Form enhancements
function initializeForms() {
    // Add loading state to form submissions
    const forms = document.querySelectorAll('form');
    forms.forEach(form => {
        form.addEventListener('submit', function () {
            const submitBtn = form.querySelector('button[type="submit"]');
            if (submitBtn && !submitBtn.disabled) {
                const originalText = submitBtn.innerHTML;
                submitBtn.disabled = true;
                submitBtn.innerHTML = '<i class="fas fa-spinner fa-spin me-2"></i>Processing...';

                // Re-enable after 10 seconds as fallback
                setTimeout(() => {
                    submitBtn.disabled = false;
                    submitBtn.innerHTML = originalText;
                }, 10000);
            }
        });
    });

    // Number input validation
    const numberInputs = document.querySelectorAll('input[type="number"]');
    numberInputs.forEach(input => {
        input.addEventListener('input', function () {
            const min = parseFloat(this.min) || 0;
            const max = parseFloat(this.max) || Infinity;
            let value = parseFloat(this.value);

            if (isNaN(value)) return;

            if (value < min) {
                this.value = min;
            } else if (value > max) {
                this.value = max;
            }
        });
    });

    // Auto-resize textareas
    const textareas = document.querySelectorAll('textarea');
    textareas.forEach(textarea => {
        textarea.addEventListener('input', function () {
            this.style.height = 'auto';
            this.style.height = (this.scrollHeight) + 'px';
        });
    });
}

// Table enhancements
function initializeTables() {
    // Add hover effects to table rows
    const tableRows = document.querySelectorAll('.table tbody tr');
    tableRows.forEach(row => {
        row.addEventListener('mouseenter', function () {
            this.style.backgroundColor = 'rgba(26, 58, 82, 0.05)';
        });

        row.addEventListener('mouseleave', function () {
            this.style.backgroundColor = '';
        });
    });

    // Confirm delete actions
    const deleteLinks = document.querySelectorAll('a[href*="/Delete"], .btn-outline-danger');
    deleteLinks.forEach(link => {
        if (link.getAttribute('href') && link.getAttribute('href').includes('/Delete')) {
            link.addEventListener('click', function (e) {
                if (!confirm('Are you sure you want to delete this item? This action cannot be undone.')) {
                    e.preventDefault();
                }
            });
        }
    });
}

// Initialize tooltips
function initializeTooltips() {
    const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });
}

// Utility functions
const AdminUtils = {
    // Show notification
    showNotification: function (message, type = 'info', duration = 5000) {
        const alertClass = type === 'success' ? 'alert-success' :
            type === 'error' ? 'alert-danger' :
                type === 'warning' ? 'alert-warning' : 'alert-info';

        const alertHtml = `
            <div class="alert ${alertClass} alert-dismissible fade show position-fixed" 
                 style="top: 20px; right: 20px; z-index: 9999; min-width: 300px;" role="alert">
                <i class="fas ${type === 'success' ? 'fa-check-circle' :
                type === 'error' ? 'fa-exclamation-circle' :
                    type === 'warning' ? 'fa-exclamation-triangle' : 'fa-info-circle'} me-2"></i>
                ${message}
                <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
            </div>
        `;

        document.body.insertAdjacentHTML('beforeend', alertHtml);

        // Auto-remove after duration
        if (duration > 0) {
            setTimeout(() => {
                const alert = document.querySelector('.alert.position-fixed');
                if (alert) {
                    const bsAlert = new bootstrap.Alert(alert);
                    bsAlert.close();
                }
            }, duration);
        }
    },

    // Format currency
    formatCurrency: function (amount) {
        return new Intl.NumberFormat('en-IN', {
            style: 'currency',
            currency: 'INR',
            minimumFractionDigits: 0,
            maximumFractionDigits: 0
        }).format(amount);
    },

    // Format date
    formatDate: function (date, options = {}) {
        const defaultOptions = {
            year: 'numeric',
            month: 'short',
            day: 'numeric'
        };

        return new Intl.DateTimeFormat('en-IN', { ...defaultOptions, ...options }).format(new Date(date));
    },

    // Debounce function
    debounce: function (func, wait) {
        let timeout;
        return function executedFunction(...args) {
            const later = () => {
                clearTimeout(timeout);
                func(...args);
            };
            clearTimeout(timeout);
            timeout = setTimeout(later, wait);
        };
    },

    // Show loading state
    showLoading: function (element) {
        element.classList.add('loading');
        element.style.pointerEvents = 'none';
    },

    // Hide loading state
    hideLoading: function (element) {
        element.classList.remove('loading');
        element.style.pointerEvents = 'auto';
    },

    // Confirm action
    confirmAction: function (message, callback) {
        if (confirm(message)) {
            callback();
        }
    },

    // Copy to clipboard
    copyToClipboard: function (text) {
        navigator.clipboard.writeText(text).then(() => {
            this.showNotification('Copied to clipboard!', 'success', 2000);
        }).catch(() => {
            this.showNotification('Failed to copy to clipboard', 'error', 3000);
        });
    }
};

// Dashboard specific functions
const Dashboard = {
    // Update statistics
    updateStats: function () {
        // This would typically fetch updated stats from the server
        console.log('Updating dashboard statistics...');
    },

    // Refresh recent orders
    refreshRecentOrders: function () {
        // This would typically fetch recent orders from the server
        console.log('Refreshing recent orders...');
    },

    // Initialize dashboard
    init: function () {
        // Auto-refresh dashboard data every 5 minutes
        setInterval(() => {
            this.updateStats();
            this.refreshRecentOrders();
        }, 300000); // 5 minutes
    }
};

// Furniture management functions
const FurnitureManager = {
    // Toggle availability
    toggleAvailability: function (furnitureId) {
        AdminUtils.confirmAction('Toggle availability for this furniture item?', () => {
            // Make AJAX request to toggle availability
            console.log('Toggling availability for furniture:', furnitureId);
            AdminUtils.showNotification('Availability updated successfully!', 'success');
        });
    },

    // Update stock
    updateStock: function (furnitureId, newStock) {
        // Make AJAX request to update stock
        console.log('Updating stock for furniture:', furnitureId, 'New stock:', newStock);
        AdminUtils.showNotification('Stock updated successfully!', 'success');
    },

    // Bulk actions
    bulkAction: function (action, selectedIds) {
        const actionText = action === 'delete' ? 'delete' :
            action === 'activate' ? 'activate' : 'deactivate';

        AdminUtils.confirmAction(`Are you sure you want to ${actionText} ${selectedIds.length} items?`, () => {
            console.log('Bulk action:', action, 'Items:', selectedIds);
            AdminUtils.showNotification(`Bulk ${actionText} completed!`, 'success');
        });
    }
};

// Order management functions
const OrderManager = {
    // Update order status
    updateStatus: function (orderId, newStatus) {
        AdminUtils.confirmAction(`Update order status to ${newStatus}?`, () => {
            // Make AJAX request to update status
            console.log('Updating order status:', orderId, 'New status:', newStatus);
            AdminUtils.showNotification('Order status updated successfully!', 'success');
        });
    },

    // Print order
    printOrder: function (orderId) {
        window.print();
    },

    // Export orders
    exportOrders: function (format = 'csv') {
        console.log('Exporting orders in format:', format);
        AdminUtils.showNotification('Export started! Download will begin shortly.', 'info');
    }
};

// Customer management functions
const CustomerManager = {
    // Toggle customer status
    toggleStatus: function (customerId) {
        AdminUtils.confirmAction('Toggle customer status?', () => {
            console.log('Toggling customer status:', customerId);
            AdminUtils.showNotification('Customer status updated successfully!', 'success');
        });
    },

    // Send notification to customer
    sendNotification: function (customerId, message) {
        console.log('Sending notification to customer:', customerId, 'Message:', message);
        AdminUtils.showNotification('Notification sent successfully!', 'success');
    }
};

// Search functionality
const Search = {
    // Initialize search
    init: function () {
        const searchInputs = document.querySelectorAll('input[type="search"], input[name="search"]');
        searchInputs.forEach(input => {
            input.addEventListener('input', AdminUtils.debounce(this.performSearch.bind(this), 300));
        });
    },

    // Perform search
    performSearch: function (event) {
        const query = event.target.value.trim();
        if (query.length >= 2) {
            console.log('Searching for:', query);
            // Implement search logic here
        }
    }
};

// Initialize search when DOM is loaded
document.addEventListener('DOMContentLoaded', function () {
    Search.init();

    // Initialize dashboard if on dashboard page
    if (document.querySelector('.dashboard-header')) {
        Dashboard.init();
    }
});

// Export functions for global access
window.AdminUtils = AdminUtils;
window.Dashboard = Dashboard;
window.FurnitureManager = FurnitureManager;
window.OrderManager = OrderManager;
window.CustomerManager = CustomerManager;

// Report Chart functions
const ReportCharts = {
    // Initialize Category Reports Charts
    initCategoryCharts: function (labels, revenueData, itemsData) {
        // Revenue Bar Chart
        const ctx1 = document.getElementById('categoryRevenueChart');
        if (ctx1) {
            new Chart(ctx1.getContext('2d'), {
                type: 'bar',
                data: {
                    labels: labels,
                    datasets: [{
                        label: 'Revenue (₹)',
                        data: revenueData,
                        backgroundColor: [
                            'rgba(13, 110, 253, 0.8)',
                            'rgba(25, 135, 84, 0.8)',
                            'rgba(220, 53, 69, 0.8)',
                            'rgba(255, 193, 7, 0.8)',
                            'rgba(13, 202, 240, 0.8)',
                            'rgba(111, 66, 193, 0.8)',
                            'rgba(253, 126, 20, 0.8)',
                            'rgba(32, 201, 151, 0.8)'
                        ],
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            display: false
                        }
                    },
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    }
                }
            });
        }

        // Items Pie Chart
        const ctx2 = document.getElementById('categoryItemsChart');
        if (ctx2) {
            new Chart(ctx2.getContext('2d'), {
                type: 'pie',
                data: {
                    labels: labels,
                    datasets: [{
                        data: itemsData,
                        backgroundColor: [
                            '#0d6efd', '#198754', '#dc3545', '#ffc107', '#0dcaf0',
                            '#6f42c1', '#fd7e14', '#20c997', '#6c757d', '#d63384'
                        ]
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            position: 'right'
                        }
                    }
                }
            });
        }
    }
};

window.ReportCharts = ReportCharts;
