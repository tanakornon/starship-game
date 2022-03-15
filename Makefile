
DIRECTORIES = Library Temp Obj Build Logs
FILE_EXTENSIONS = *.log *.bak *.swp
UNITY_CACHE = $(HOME)/.cache/unity3d

clean:
	@echo "Cleaning Unity project..."

	# Remove specified directories
	@for dir in $(DIRECTORIES); do \
		if [ -d $$dir ]; then \
			echo "Deleting directory: $$dir"; \
			rm -rf $$dir; \
		fi \
	done

	# Remove unwanted file extensions
	@echo "Removing unwanted file types..."
	@find . -type f \( -name $(FILE_EXTENSIONS) \) -exec rm -f {} \;

	# Clean Unity's Cache (optional)
	@echo "Cleaning Unity cache..."
	@rm -rf $(UNITY_CACHE)

	@echo "Cleanup completed."
