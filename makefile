PROGRAM   = fprog-project
CXX       = clang++
CXXFLAGS  = -g -std=c++17 -Wall
INCLUDES = -I/home/fran/some_directory


$(PROGRAM): main.cpp
	$(CXX) $(CXXFLAGS) $(INCLUDES) pythagoras.cpp -o $(PROGRAM)

.PHONY: clean 

clean:
	-rm -f *.o $(PROGRAM) 

dist: clean
	-tar -chvj -C .. -f ../$(PROGRAM).tar.bz2 $(PROGRAM)


