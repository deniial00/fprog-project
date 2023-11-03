#include <iostream>
#include <vector>

// make file to include library
#include <range/v3/all.hpp>

int main(int argc, char* argv[]) {
    std::vector<int> v{1,2,3,4};
    ranges::sort( v );
}	
