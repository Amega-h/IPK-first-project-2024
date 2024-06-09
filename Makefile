all: publish clean

publish:
	dotnet publish -r linux-x64 -c Release -o .
clean:
	rm -rf bin
	rm -rf obj