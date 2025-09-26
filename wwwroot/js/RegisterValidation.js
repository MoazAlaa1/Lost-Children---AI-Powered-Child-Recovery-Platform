 // Password strength indicator
        document.getElementById('password').addEventListener('input', function() {
            const password = this.value;
            const strengthBar = document.getElementById('passwordStrengthBar');
            let strength = 0;
            
            // Check length
            const lengthReq = document.getElementById('lengthReq');
            if (password.length >= 8) {
                strength += 25;
                lengthReq.classList.add('valid');
                lengthReq.querySelector('i').className = 'fas fa-check-circle';
            } else {
                lengthReq.classList.remove('valid');
                lengthReq.querySelector('i').className = 'fas fa-circle';
            }
            
            // Check for numbers
            const numberReq = document.getElementById('numberReq');
            if (/\d/.test(password)) {
                strength += 25;
                numberReq.classList.add('valid');
                numberReq.querySelector('i').className = 'fas fa-check-circle';
            } else {
                numberReq.classList.remove('valid');
                numberReq.querySelector('i').className = 'fas fa-circle';
            }
            
            // Check for special chars
            const specialReq = document.getElementById('specialReq');
            if (/[!@#$%^&*(),.?":{}|<>]/.test(password)) {
                strength += 25;
                specialReq.classList.add('valid');
                specialReq.querySelector('i').className = 'fas fa-check-circle';
            } else {
                specialReq.classList.remove('valid');
                specialReq.querySelector('i').className = 'fas fa-circle';
            }
            
            // Check for uppercase
            const UpperReq = document.getElementById('UpperReq');
            if (/[A-Z]/.test(password)) {
                strength += 25;
                UpperReq.classList.add('valid');
                UpperReq.querySelector('i').className = 'fas fa-check-circle';
            }
            else {
                UpperReq.classList.remove('valid');
                UpperReq.querySelector('i').className = 'fas fa-circle';
            }
            
            // Update strength bar
            strengthBar.style.width = strength + '%';
            
            // Change color based on strength
            if (strength < 50) {
                strengthBar.style.backgroundColor = '#dc3545'; // Red
            } else if (strength < 75) {
                strengthBar.style.backgroundColor = '#fd7e14'; // Orange
            } else {
                strengthBar.style.backgroundColor = '#28a745'; // Green
            }
        });
        
        // Password match validation
        document.getElementById('confirmPassword').addEventListener('input', function() {
            const password = document.getElementById('password').value;
            const confirmPassword = this.value;
            const feedback = document.getElementById('passwordMatchFeedback');
            
            if (confirmPassword && password !== confirmPassword) {
                this.classList.add('is-invalid');
                feedback.style.display = 'block';
            } else {
                this.classList.remove('is-invalid');
                feedback.style.display = 'none';
            }
        });
        
        // Form submission
        document.querySelector('.register-form').addEventListener('submit', function(e) {
            e.preventDefault();
            
            // Validate password match
            const password = document.getElementById('password').value;
            const confirmPassword = document.getElementById('confirmPassword').value;
            
            if (password !== confirmPassword) {
                document.getElementById('confirmPassword').classList.add('is-invalid');
                document.getElementById('passwordMatchFeedback').style.display = 'block';
                return;
            }
            
            this.submit();
        });